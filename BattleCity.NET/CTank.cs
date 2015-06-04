using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace BattleCity.NET
{
    class CTank : IDisposable
    {
        private static readonly Image wrecked = Image.FromFile(@"Images\wrecked.png")
            .GetThumbnailImage(CConstants.tankSize, CConstants.tankSize, null, IntPtr.Zero);

        public CTank(string dll, string image, List<CTank> tanks)
        {
            m_ai = new CTankAI(dll);
            m_graphics = new CTankGraphics(image, CConstants.tankSize, CConstants.turretSize, 50);

            for (int tryCnt = 0; tryCnt < 100; ++tryCnt)
            {
                m_x = CRandom.Next(CConstants.formWidth - CConstants.tankSize) + CConstants.tankSize / 2;
                m_y = CRandom.Next(CConstants.formHeight - CConstants.tankSize) + CConstants.tankSize / 2;

                if (PlacementIsFree(m_x, m_y, tanks))
                {
                    break;
                }
            }

            m_health = 100;
            m_baseDirection = CRandom.Next(360);
            m_turretDirection = m_baseDirection;
            m_deadPlace = -1;
            m_reload = CConstants.reloadTime;
            m_destroyed = false;
        }
        public void Dispose() 
        {
            m_ai.Dispose();
        }
        private bool PlacementIsFree(double x, double y, List<CTank> tanks)
        {
            for (int i = 0; i < tanks.Count; i++)
            {
                if (tanks[i].CheckCollision(x, y, 2 * CConstants.tankSize))
                {
                    return false;
                }
            }
            return true;
        }
        private List<CTank> GetEnemies(List<CTank> tanks)
        {
            List<CTank> enemies = new List<CTank>();
            for (int i = 0; i < tanks.Count; i++)
            {
                int x0 = Convert.ToInt32(tanks[i].m_x - m_x); //проекция вектора, соединяющего текущий танк со врагом на ось х
                int y0 = -Convert.ToInt32(tanks[i].m_y - m_y); //проекция вектора, соединяющего текущий танк со врагом на ось у
                double angle = -Math.Atan2(y0, x0) * 180 / Math.PI + 90;//угол этого самого вектора
                while (angle < 360)
                {
                    angle += 360;
                }
                while (m_baseDirection < 360)
                {
                    m_baseDirection += 360;
                }
                if ((angle > (m_baseDirection - 45) && angle < (m_baseDirection + 45))
                    || ((angle % 360) > (m_baseDirection - 45) && (angle % 360) < (m_baseDirection + 45))
                    || (angle > (m_baseDirection % 360 - 45) && angle < (m_baseDirection % 360 + 45)))
                {
                    if (!(tanks[i].m_x == m_x && tanks[i].m_y == m_y) && !tanks[i].IsDead())//это мы сами или труп
                    {
                        enemies.Add(tanks[i]);
                    }
                }
                m_baseDirection %= 360;
            }
            return enemies;
        }
        private void TryToMoveForward(double x, double y, List<CTank> tanks)
        {
            bool ableToMove = true;
            bool ableToMoveX = true;
            bool ableToMoveY = true;
            for(int i = 0; i < tanks.Count; i++)
            {
                if (tanks[i] != this)
                {
                    if (tanks[i].CheckCollision(m_x + x, m_y - y, CConstants.tankSize))
                    {
                        ableToMove = false;
                        if (tanks[i].IsDead())
                        {
                            tanks[i].m_destroyed = true;
                            ableToMove = true;
                        }
                    }
                }
            }
            if (ableToMove)
            {
                m_x += x;
                m_y -= y;
                return;
            }
            for (int i = 0; i < tanks.Count; i++)
            {
                if (tanks[i] != this)
                {
                    if (tanks[i].CheckCollision(m_x + x, m_y, CConstants.tankSize))
                    {
                        ableToMoveX = false;
                    }
                }
            }
            if (ableToMoveX)
            {
                m_x += x;
                return;
            }
            for (int i = 0; i < tanks.Count; i++)
            {
                if (tanks[i] != this)
                {
                    if (tanks[i].CheckCollision(m_x, m_y - y, CConstants.tankSize))
                    {
                        ableToMoveY = false;
                    }
                }
            }
            if (ableToMoveY)
            {
                m_y -= y;
            }
        }
        public void Actions(List<CTank> tanks, List<CShell> shells, CManagerMedChest medChests)
        {
            if (IsDead())
            {
                m_reload = 0;
                return;
            }
            if (m_reload > 0)
            {
                m_reload--;
            }
            

            int distance = -1;

            {
                List<CMedicineChest> allChest = medChests.GetMedicineChests();
                int countTemp = allChest.Count();
                m_ai.setVisibleChests(countTemp);
                for (int i = 0; i < countTemp; ++i)
                {
                    m_ai.setCoordinatesChest(i, allChest[i].GetX(), allChest[i].GetY());
                }
            }

            m_ai.setCoords(Convert.ToInt32(m_x), Convert.ToInt32(m_y));
            m_ai.setAngle(m_baseDirection); 
            m_ai.setTurretAngle(m_turretDirection);
            m_ai.setCollisionStatus(DetectCollisions(tanks));
            m_ai.setLivePercent(m_health);

            {
                List<CTank> visibleEnemies = GetEnemies(tanks);
                m_ai.setVisilbeEnemyCount(visibleEnemies.Count);
                for (int i = 0; i < visibleEnemies.Count; i++)
                {
                    m_ai.setEnemyProteries(i, Convert.ToInt32(visibleEnemies[i].m_x), Convert.ToInt32(visibleEnemies[i].m_y),
                        visibleEnemies[i].m_baseDirection, visibleEnemies[i].m_turretDirection, visibleEnemies[i].m_health);
                }
            }

            m_ai.update();
            m_baseDirection += Convert.ToInt32(LimitValue(m_ai.getRotateDirection(), -1, 1) * LimitRotateSpeed(m_ai.getRotateSpeed()) * CConstants.baseRotationRate + 360) % 360;

            {
                int newDirection = LimitValue(m_ai.getDirection(), -1, 1);
                TryToMoveForward(newDirection * GetTankSpeed() * Math.Sin(m_baseDirection * Math.PI / 180),
                    newDirection * GetTankSpeed() * Math.Cos(m_baseDirection * Math.PI / 180), tanks);

                FixCollisions(tanks);
                m_turretDirection += Convert.ToInt32(LimitValue(m_ai.getTurretRotateDirection(), -1, 1) * LimitTurretRotateSpeed(m_ai.getTurretRotateSpeed()) * CConstants.turretRotationRate + 360) % 360;

                distance = m_ai.getFireDistance();
                if (distance < -1)
                {
                    distance = -1;
                }
            }
            if (distance != -1 && m_reload == 0)
            {
                shells.Add(new CShell(Convert.ToInt32(m_x), Convert.ToInt32(m_y), m_turretDirection, distance, this));
                m_reload = CConstants.reloadTime;
                ++m_shots;
            }
        }
        public void Draw(Graphics graph)
        {
            if (m_destroyed)
            {
                return;
            }
            if (!IsDead())
            {
                graph.DrawImage(m_graphics.base_, FBattleScreen.GetRotatedRectangle(m_baseDirection, CConstants.tankSize, m_x, m_y));
                graph.DrawImage(m_graphics.turret, FBattleScreen.GetRotatedRectangle(m_turretDirection, CConstants.turretSize, m_x, m_y));
            }
            else
            {
                graph.DrawImage(wrecked, FBattleScreen.GetRotatedRectangle(m_baseDirection, CConstants.tankSize, m_x, m_y));
            }
        }
        private void SetProgressBar(CProgressBar progress, int offset, int valPreaload, Color color)
        {
            progress.Size = new Size(100, 10);
            int x = (int)m_x - CConstants.tankSize / 2;
            int y = (int)m_y - offset;
            progress.Location = new Point(x, y);
            progress.Value = valPreaload;
            progress.ForeColor = Color.Transparent;
            progress.BackColor = Color.Transparent;
            progress.BorderWidth = 2;
            progress.BorderColor = Color.White;
            progress.ProgressColor = color;
            if (m_health == 0)
            {
                progress.Visible = false;
            }
            else
            {
                progress.Visible = true;
                progress.Show();
            }
            

        }


        public void SetTankInfo(ProgressBar pbHealth, ProgressBar pbReload, Label hits, Label condition, PictureBox pbox, GroupBox gb, CProgressBar progressBar, CProgressBar progressBarHealth, bool disableSidePb)
        {
            pbHealth.Value = m_health;
            pbReload.Value = m_reload * pbReload.Maximum / CConstants.reloadTime;
            Color color =  (m_health < 40 ) ? Color.Red : Color.Green;
            SetProgressBar(progressBar, CConstants.tankSize / 2, pbReload.Value, Color.Green);
            SetProgressBar(progressBarHealth, CConstants.tankSize / 2 + 10, pbHealth.Value, color);
            hits.Text = Convert.ToString(m_hits);
            if (!IsDead())
            {
                condition.Text = "Still alive";
            }
            else
            {
                condition.Text = "Dead " + Convert.ToString(m_deadPlace);
            }
            pbox.Image = m_graphics.tank;

            if (!disableSidePb)
            {
                gb.Visible = true;
            }
        }
        public bool CheckCollision(double x, double y, int length)
        {
            if (!m_destroyed && Math.Abs(m_x - x) < length && Math.Abs(m_y - y) < length)
            {
                return true;
            }
            return false;
        }
        public bool FixCollisions(List<CTank> tanks)
        {
            bool result = false;
            if (m_x < CConstants.tankSize / 2)
            {
                m_x = CConstants.tankSize / 2;
                result = true;
            }
            if (m_x > CConstants.formWidth - CConstants.tankSize / 2)
            {
                m_x = CConstants.formWidth - CConstants.tankSize / 2;
                result = true;
            }
            if (m_y < CConstants.tankSize / 2)
            {
                m_y = CConstants.tankSize / 2;
                result = true;
            }
            if (m_y > CConstants.formHeight - CConstants.tankSize / 2)
            {
                m_y = CConstants.formHeight - CConstants.tankSize / 2;
                result = true;
            }
            for (int i = 0; i < tanks.Count; i++)
            {
                if (tanks[i].CheckCollision(m_x, m_y, CConstants.tankSize / 2) && !(m_x == tanks[i].m_x && m_y == tanks[i].m_y))
                {
                    result = true;
                }
            }
            return result;
        }
        public bool DetectCollisions(List<CTank> tanks)
        {
            if (m_x <= CConstants.tankSize / 2 || m_y <= CConstants.tankSize / 2 || m_x >= CConstants.formWidth - CConstants.tankSize / 2 ||
                m_y >= CConstants.formHeight - CConstants.tankSize / 2)
            {
                return true; //collision with borders
            }
            for (int i = 0; i < tanks.Count; i++)
            {
                if (tanks[i].CheckCollision(m_x, m_y, CConstants.tankSize + GetTankSpeed()))//collision with tanks
                {
                    if (!(tanks[i] == this))//it's not me
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void SetDamage(short damage)
        {
            if(m_health < 0)
            {
                return;
            }

            m_health -= damage;
            if (m_health < 0)
            {
                m_health = 0;
                FBattleScreen.PlaySound("player_death");
            }

            m_dmgTaken += damage;
            ++m_hitsTaken;
        }

        public double GetX()
        {
            return m_x;
        }
        public double GetY()
        {
            return m_y;
        }


        public void SetHealth(short health)
        {
            if (IsDead())
            {
                return;
            }

            if (m_health <= 90)
            {
                m_health += health;
            }
            else if (m_health > 90)
            {
                m_health = 100;
            }

            ++m_heals;
        }

        public bool IsDead() { return m_health <= 0; }
        public void SetDeadPlace(short deadNumber) { m_deadPlace = deadNumber; }
        public void SuccessfulHit() { m_hits++; }
        private readonly CTankAI m_ai;
        private bool m_destroyed;
        private readonly CTankGraphics m_graphics;
        private double m_x;
        private double m_y;
        private short m_health;
        public bool m_slow = false;

        private short m_hits = 0;
        public int m_shots = 0;
        public int m_heals = 0;
        public int m_dmgTaken = 0;
        public int m_hitsTaken = 0;

        private int m_reload;
        private int m_baseDirection;
        private int m_turretDirection;
        private short m_deadPlace;

        private static int LimitValue(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
        }

        private int LimitRotateSpeed(int value)
        {
            if (!m_slow)
            {
                return LimitValue(value, 0, 10);
            }
            else
            {
                return LimitValue(value, 0, 5);
            }
        }

        private int LimitTurretRotateSpeed(int value)
        {
            if (!m_slow)
            {
                return LimitValue(value, 0, 20);
            }
            else
            {
                return LimitValue(value, 0, 10);
            }
        }

        private int GetTankSpeed()
        {
            if (!m_slow)
            {
                return CConstants.tankSpeed;
            }
            else
            {
                return CConstants.tankSpeed / 2;
            }
        }
    }
}
