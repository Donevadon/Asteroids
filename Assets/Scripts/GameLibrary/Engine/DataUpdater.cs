using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace GameLibrary.Engine
{
    class DataUpdater : IDataUpdater
    {
        private readonly int CycleInterval = 20;
        private static List<IEntity> GameEntities;
        private static DataUpdater dataUpdater;
        private Thread thread;

        private DataUpdater(ref Action program_Closed)
        {
            thread = new Thread(Update);
            GameEntities = new List<IEntity>();
            program_Closed += Stop;
            Start();
        }

        public static DataUpdater GetInstance(ref Action program_Closed)
        {
            if (dataUpdater is null) dataUpdater = new DataUpdater(ref program_Closed);
            return dataUpdater;
        }

        public void Add(IEntity entity)
        {
            GameEntities.Add(entity);
        }

        public void Remove(IEntity entity)
        {
            GameEntities.Remove(entity);
        }

        private void Start()
        {
            thread.Start();
        }

        private void Stop()
        {
            thread.Abort();
        }

        private void Update()
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i < GameEntities.Count; i++)
                    {
                        GameEntities[i].UpdateData();
                    }
                }
                catch (Exception ex) 
                {
                    
                }
                Thread.Sleep(CycleInterval);
            }
        }

        public void RemoveAll()
        {
            GameEntities.RemoveAll((x) => true);
        }

        public void RemoveAt(Entity entityType)
        {
            GameEntities.RemoveAll((x) => x.Type == entityType);
        }

        public IEntity[] FindEntityAt<T>() where T : IEntity
        {
            return GameEntities.FindAll((x) => x is T).ToArray();
        }
    }
}
