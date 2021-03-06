﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Diagnostics;

namespace BattleCity.NET
{
    public partial class FBattleScreen : Form
    {
        public FBattleScreen(List<string> dlls, bool disableGamePb, bool disableSidePb, int playerCntAim = 1, List<string> winnerDlls = null, List<string> deadDlls = null)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
               ControlStyles.UserPaint |
               ControlStyles.DoubleBuffer, true);
            this.BackgroundImage = Image.FromFile(@"Images\fon.png");
            this.BackgroundImageLayout = ImageLayout.None;
            timer1.Interval = CConstants.refreshTime;
            this.Width = CConstants.formWidth + 218;
            this.Height = CConstants.formHeight + 47;
            shells = new List<CShell>();
            tanks = new List<CTank>();
            explosions = new List<CExplosion>();
            PlaySound("level_start");
            m_medChests = new CManagerMedChest(tanks);
            int countTanks = dlls.Count();

            for (int i = 0; i < countTanks; ++i)
            {
                listProgressBar.Add(new CProgressBar());
                listProgressBarHealth.Add(new CProgressBar());

                if (!disableGamePb)
                {
                    this.Controls.Add(listProgressBar[i]);
                    this.Controls.Add(listProgressBarHealth[i]);
                }
            }

            if (countTanks > 0)
                gbPlayer1.Text = Path.GetFileNameWithoutExtension(dlls[0]);
            if (countTanks > 1)
                gbPlayer2.Text = Path.GetFileNameWithoutExtension(dlls[1]);
            if (countTanks > 2)
                gbPlayer3.Text = Path.GetFileNameWithoutExtension(dlls[2]);
            if (countTanks > 3)
                gbPlayer4.Text = Path.GetFileNameWithoutExtension(dlls[3]);

            m_disableSidePb = disableSidePb;
            m_winnerDlls = winnerDlls;
            m_deadDlls = deadDlls;
            m_playerCntAim = playerCntAim;

            Directory.CreateDirectory("tmp");

            try
            {
                for (int i = 0; i < dlls.Count; i++)
                {
                    File.Copy(dlls[i], "tmp/tempDLL" + Convert.ToString(i) + ".dll", true);
                    NewTank("tmp/tempDLL" + Convert.ToString(i) + ".dll", dlls[i], NumberToColor(i) + ".png");
                }
            }
            catch (Exception e)
            {
                DisposeOfTanks();
                throw e;
            }

            timer1.Enabled = true;
        }

        private string NumberToColor(int num)
        {
            switch (num)
            {
                case 0:
                    return "blue";
                case 1:
                    return "green";
                case 2:
                    return "red";
                case 3:
                    return "yellow";
                default:
                    Debug.Assert(false);
                    return "";
            }
        }

        List<CProgressBar> listProgressBar = new List<CProgressBar>();
        List<CProgressBar> listProgressBarHealth = new List<CProgressBar>();
        private List<CTank> tanks;
        private List<CShell> shells;
        private List<CExplosion> explosions;
        private short deadPlace = 1;
        private CManagerMedChest m_medChests;
        private readonly bool m_disableSidePb;
        private List<string> m_winnerDlls;
        private List<string> m_deadDlls;
        private readonly int m_playerCntAim;

        public static Point[] GetRotatedRectangle(int degree, int size, double x0, double y0)
        {
            int x = -size / 2;
            int y = size / 2;
            double x1 = Math.Cos(degree * Math.PI / 180) * x + Math.Sin(degree * Math.PI / 180) * y;
            double y1 = Math.Sin(degree * Math.PI / 180) * x - Math.Cos(degree * Math.PI / 180) * y;
            x = size / 2;
            y = size / 2;
            double x2 = Math.Cos(degree * Math.PI / 180) * x + Math.Sin(degree * Math.PI / 180) * y;
            double y2 = Math.Sin(degree * Math.PI / 180) * x - Math.Cos(degree * Math.PI / 180) * y;
            x = -size / 2;
            y = -size / 2;
            double x3 = Math.Cos(degree * Math.PI / 180) * x + Math.Sin(degree * Math.PI / 180) * y;
            double y3 = Math.Sin(degree * Math.PI / 180) * y - Math.Cos(degree * Math.PI / 180) * y;
            return new Point[] { new Point(Convert.ToInt32(x0 + x1), Convert.ToInt32(y0 + y1)),
                new Point(Convert.ToInt32(x0 + x2), Convert.ToInt32(y0 + y2)), new Point(Convert.ToInt32(x0 + x3), Convert.ToInt32(y0 + y3)) };
        }

        public static void PlaySound(string path)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"Sound/" + path + ".wav";
            try
            {
                player.Play();
            }
            catch
            {
                return;
            }
        }

        public void NewTank(string dll, string name, string image)
        {
            tanks.Add(new CTank(dll, name, image, tanks));
        }

        private void RefreshInterface(bool disableSidePb)
        {
          
            if (tanks.Count < 1) return;
            tanks[0].SetTankInfo(pbPlayer1Health, pbPlayer1Reload, lPlayer1Hits, lPlayer1Condition, pbTank1Image, gbPlayer1, listProgressBar[0], listProgressBarHealth[0], disableSidePb);
            if (tanks.Count < 2) return;
            tanks[1].SetTankInfo(pbPlayer2Health, pbPlayer2Reload, lPlayer2Hits, lPlayer2Condition, pbTank2Image, gbPlayer2, listProgressBar[1], listProgressBarHealth[1], disableSidePb);
            if (tanks.Count < 3) return;
            tanks[2].SetTankInfo(pbPlayer3Health, pbPlayer3Reload, lPlayer3Hits, lPlayer3Condition, pbTank3Image, gbPlayer3, listProgressBar[2], listProgressBarHealth[2], disableSidePb);
            if (tanks.Count < 4) return;
            tanks[3].SetTankInfo(pbPlayer4Health, pbPlayer4Reload, lPlayer4Hits, lPlayer4Condition, pbTank4Image, gbPlayer4, listProgressBar[3], listProgressBarHealth[3], disableSidePb);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i < tanks.Count; i++)
            {
                tanks[i].Draw(e.Graphics);
            }
            for (int i = 0; i < shells.Count; i++)
            {
                shells[i].DrawTrack(e.Graphics);
                if (shells[i].IsVisible())
                    shells[i].Draw(e.Graphics);
            }
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Draw(e.Graphics);
            }
            m_medChests.DrawAllMedchests(e.Graphics, tanks);
        }
        private void RefreshTanks()
        {
            for (int i = 0; i < tanks.Count; i++)
            {
                tanks[i].Actions(tanks, shells, m_medChests);
            }
        }

        private void HandleSetDamage(CTank tank, short damage)
        {
            if (tank.SetDamage(damage) && m_deadDlls != null)
            {
                m_deadDlls.Add(tank.m_name);
            }
        }

        private void ExplosionDamage(double x, double y, CTank tank, CShell shell)
        {
            if (tank != null && !tank.IsDead())
            {
                if (tank.CheckCollision(x, y, CConstants.tankSize / 2)) //прямое попадание
                {
                    HandleSetDamage(tank, 10);
                }
                if (tank.CheckCollision(x, y, CConstants.tankSize)) //в половине корпуса от танка
                {
                    HandleSetDamage(tank, 5);
                }
                if (tank.CheckCollision(x, y, 3 * CConstants.tankSize / 2)) //в корпусе от танка
                {
                    HandleSetDamage(tank, 5);
                    shell.SuccessfulyHits();
                }
                if (tank.IsDead())
                {
                    tank.SetDeadPlace(deadPlace);
                    deadPlace++;
                }
            }
        }

        private void RefreshShells()
        {
            for (int i = 0; i < shells.Count; i++)
            {
                if (shells[i].IsVisible())
                {
                    shells[i].MoveShell();
                    if (shells[i].IsExploded())
                    {
                        for (int j = 0; j < tanks.Count; j++)
                        {
                            ExplosionDamage(shells[i].GetX(), shells[i].GetY(), tanks[j], shells[i]);
                        }
                        explosions.Add(shells[i].GetExplosion());
                    }
                    if (shells[i].OutOfField() || shells[i].IsExploded())
                    {
                        //   shells.RemoveAt(i);
                       shells[i].SetNotVisible();
                
                    }
                }
                else if (!shells[i].TrackIsVisible())
                {
                        shells.RemoveAt(i);
                } 
            }
        }
        public void RefreshExplosions()
        {
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Update();
                if (explosions[i].ObjectIsDead())
                {
                    explosions.RemoveAt(i);
                }
            }
        }

        private bool GameOver()
        {
            int alivePlayers = 0;
            for(int i = 0; i < tanks.Count; i++)
            {
                if (!tanks[i].IsDead())
                {
                    alivePlayers++;
                }
            }
            return (alivePlayers < m_playerCntAim + 1);
        }

        private string GetWinner()
        {
            int winnerIndex = -1;
            int winnerCount = 0;

            for (int i = 0; i < tanks.Count; ++i)
            {
                if (!tanks[i].IsDead())
                {
                    winnerIndex = i;
                    ++winnerCount;
                }
            }

            if (winnerCount == 0)
            {
                return "No one survived";
            }
            else if (winnerCount == 1)
            {
                return "Player " + Convert.ToString(winnerIndex + 1) + " wins";
            }
            else
            {
                return "Multiple players survived";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GameOver())
            {
                Close();
                return;
            }

            m_medChests.Update();
            if (CRandom.Next(CConstants.medChestAppearRate) == 0)
            {
                m_medChests.AddMedChest();
            }

            RefreshTanks();
            RefreshShells();
            RefreshExplosions();

            if (m_frameTick == 0)
            {
                RefreshInterface(m_disableSidePb);
                this.Refresh();
            }
            m_frameTick = (m_frameTick + 1) % (m_framesToSkip + 1);
        }

        private uint m_frameTick = 0;
        private uint m_framesToSkip = 0;

        private void FBattleScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            PlaySound("game_over");
            SaveWinnerDlls();
            ShowStats();
            DisposeOfTanks();
        }

        private void SaveWinnerDlls()
        {
            if (m_winnerDlls == null)
            {
                return;
            }

            for (int i = 0; i < tanks.Count; ++i)
            {
                if (!tanks[i].IsDead())
                {
                    m_winnerDlls.Add(tanks[i].m_name);
                }
            }
        }

        private void DisposeOfTanks()
        {
            for (int i = 0; i < tanks.Count; i++)
            {
                if (tanks[i] != null)
                {
                    tanks[i].Dispose();
                    tanks[i] = null;
                }
            }
        }

        private void ShowStats()
        {
            StatsForm sf = new StatsForm();
            sf.Text = GetWinner();

            foreach (CTank tank in tanks)
            {
                sf.AddRecord(tank);
            }

            sf.AddTotalRecord();
            sf.ShowDialog();
        }
    }
}
