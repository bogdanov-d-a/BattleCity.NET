using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace BattleCity.NET
{
    class CTankAI : IDisposable
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        private static extern IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        private static extern IntPtr GetProcAddress(IntPtr library, string procName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        private static extern bool FreeLibrary(IntPtr library);

        
        private readonly string m_path;
        private IntPtr m_library;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetCoords(int x, int y);
        public readonly SetCoords setCoords;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetAngle(int angle);
        public readonly SetAngle setAngle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetTurretAngle(int angle);
        public readonly SetTurretAngle setTurretAngle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetCollisionStatus(bool isCollided);
        public readonly SetCollisionStatus setCollisionStatus;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetLivePercent(int percent);
        public readonly SetLivePercent setLivePercent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetVisilbeEnemyCount(int count);
        public readonly SetVisilbeEnemyCount setVisilbeEnemyCount;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetEnemyProteries(int enemyID, int x, int y, int angle, int turretAngle, int livePercent);
        public readonly SetEnemyProteries setEnemyProteries;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetDirection();
        public readonly GetDirection getDirection;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetRotateDirection();
        public readonly GetRotateDirection getRotateDirection;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetRotateSpeed();
        public readonly GetRotateSpeed getRotateSpeed;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetTurretRotateDirection();
        public readonly GetTurretRotateDirection getTurretRotateDirection;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetTurretRotateSpeed();
        public readonly GetTurretRotateSpeed getTurretRotateSpeed;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetFireDistance();
        public readonly GetFireDistance getFireDistance;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetVisibleChests(int count);
        public readonly SetVisibleChests setVisibleChests;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetCoordinatesChest(int id, double x, double y);
        public readonly SetCoordinatesChest setCoordinatesChest;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Update();
        public readonly Update update;


        private bool m_antibonusFunctionsLoaded;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SetVisibleAntibonusCountT(int count);
        private readonly SetVisibleAntibonusCountT m_setVisibleAntibonusCount;

        public void SetVisibleAntibonusCount(int count)
        {
            if (m_antibonusFunctionsLoaded)
            {
                m_setVisibleAntibonusCount(count);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SetVisibleAntibonusDataT(int id, double x, double y);
        private readonly SetVisibleAntibonusDataT m_setVisibleAntibonusData;

        public void SetVisibleAntibonusData(int id, double x, double y)
        {
            if (m_antibonusFunctionsLoaded)
            {
                m_setVisibleAntibonusData(id, x, y);
            }
        }

        
        private bool Loaded()
        {
            return m_library != IntPtr.Zero;
        }

        private Delegate GetProcDelegate(Type type, string procName)
        {
            Debug.Assert(Loaded());

            IntPtr addr = GetProcAddress(m_library, procName);
            if (addr == IntPtr.Zero)
            {
                throw new Exception("TankAI " + m_path + ": GetProc " + procName + " failed");
            }

            return Marshal.GetDelegateForFunctionPointer(addr, type);
        }

        public CTankAI(string path)
        {
            m_path = path;

            m_library = LoadLibrary(path);
            if (!Loaded())
            {
                throw new Exception("TankAI " + path + ": LoadLibrary failed");
            }

            try
            {
                setCoords = (SetCoords)GetProcDelegate(typeof(SetCoords), "SetCoords");
                setAngle = (SetAngle)GetProcDelegate(typeof(SetAngle), "SetAngle");
                setTurretAngle = (SetTurretAngle)GetProcDelegate(typeof(SetTurretAngle), "SetTurretAngle");
                setCollisionStatus = (SetCollisionStatus)GetProcDelegate(typeof(SetCollisionStatus), "SetCollisionStatus");
                setLivePercent = (SetLivePercent)GetProcDelegate(typeof(SetLivePercent), "SetLivePercent");
                setVisilbeEnemyCount = (SetVisilbeEnemyCount)GetProcDelegate(typeof(SetVisilbeEnemyCount), "SetVisilbeEnemyCount");
                setEnemyProteries = (SetEnemyProteries)GetProcDelegate(typeof(SetEnemyProteries), "SetEnemyProteries");
                getDirection = (GetDirection)GetProcDelegate(typeof(GetDirection), "GetDirection");
                getRotateDirection = (GetRotateDirection)GetProcDelegate(typeof(GetRotateDirection), "GetRotateDirection");
                getRotateSpeed = (GetRotateSpeed)GetProcDelegate(typeof(GetRotateSpeed), "GetRotateSpeed");
                getTurretRotateDirection = (GetTurretRotateDirection)GetProcDelegate(typeof(GetTurretRotateDirection), "GetTurretRotateDirection");
                getTurretRotateSpeed = (GetTurretRotateSpeed)GetProcDelegate(typeof(GetTurretRotateSpeed), "GetTurretRotateSpeed");
                getFireDistance = (GetFireDistance)GetProcDelegate(typeof(GetFireDistance), "GetFireDistance");
                setVisibleChests = (SetVisibleChests)GetProcDelegate(typeof(SetVisibleChests), "SetVisibleChests");
                setCoordinatesChest = (SetCoordinatesChest)GetProcDelegate(typeof(SetCoordinatesChest), "SetCoordinatesChest");
                update = (Update)GetProcDelegate(typeof(Update), "Update");
            }
            catch (Exception e)
            {
                Dispose();
                throw e;
            }

            m_antibonusFunctionsLoaded = true;
            try
            {
                m_setVisibleAntibonusCount = (SetVisibleAntibonusCountT)GetProcDelegate(typeof(SetVisibleAntibonusCountT), "SetVisibleAntibonusCount");
                m_setVisibleAntibonusData = (SetVisibleAntibonusDataT)GetProcDelegate(typeof(SetVisibleAntibonusDataT), "SetVisibleAntibonusData");
            }
            catch (Exception)
            {
                m_antibonusFunctionsLoaded = false;
            }
        }

        public void Dispose()
        {
            Debug.Assert(Loaded());

            bool success = FreeLibrary(m_library);
            Debug.Assert(success);

            m_library = IntPtr.Zero;
        }
    }
}
