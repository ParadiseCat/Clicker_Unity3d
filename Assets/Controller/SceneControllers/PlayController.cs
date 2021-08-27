using Clicker.ResourcesData;
using System;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

using Random = System.Random;

namespace Clicker
{
    /// <summary>
    /// Обновляемый контроллер сцены игры
    /// </summary>
    internal class PlayController : Controller<PlayController>, IUpdate, IRandomMoveble, IClickable
    {
        private const int MAX_SIZE = 10;

        private int fullTimeSpawn = 200;
        private int timeSpawn;
        private int hpMonster = 3;

        private int spawnMonster = 0;

        List<IRandomMove> monsterList;
        List<IClick> clickerList;
        Mushroom anim = new Mushroom();
        Random random = new Random(DateTime.UtcNow.GetHashCode());

        Vector2 minBound;
        Vector2 maxBound;

        string[] skins;
        int skinCount;
        string animRes;

        /// <summary>
        /// инициализация обновляемого объекта
        /// </summary>
        private void Start()
        {
            GameManager.Instance.SetUpdateBehaviour(this);

            monsterList = new List<IRandomMove>();
            clickerList = new List<IClick>();

            skins = new string[3]
            {
                anim.SkinMaterial(Mushroom.SkinMaterials.Blue),
                anim.SkinMaterial(Mushroom.SkinMaterials.Red),
                anim.SkinMaterial(Mushroom.SkinMaterials.Green)
            };

            skinCount = skins.Length;
            animRes = anim.Res_Path;

            minBound = new Vector2(-3.5f, -6f);
            maxBound = new Vector3(3.5f, 6f);

            timeSpawn = fullTimeSpawn;

            AddMonster();
        }

        /// <summary>
        /// удаление обновляемого объекта
        /// </summary>
        private void OnDestroy()
        {
            GameManager.Instance.ResetUpdateBehaviour(this);
            
            foreach (IRandomMove obj in monsterList)
            {
                obj.InstanceDestroy();
            }
        }

        /// <summary>
        /// определение функционала при обновлении наследника
        /// </summary>
        public virtual void OnUpdate(float deltaTime)
        {
            // выход ли?
            UpdateCheckQuit();

            // нажатие на мышь / тач
            UpdateClicker();

            // итеация движения движемых объектов
            UpdateRandomMove();

            // проверка на премет создания нового объекта
            UpdateSpawn();
        }

        private void AddMonster()
        {
            GameObject obj = new GameObject("Monster");
            Monster monster = obj.AddComponent<Monster>();
            monster.AddModel(anim.Name, anim.Res_Path, GetStartPosition(), hpMonster);
            monster.ScaleModel(new Vector3(0.25f, 0.25f, 0.25f));
            monster.AddSkin(skins[random.Next(0, skinCount)], animRes);
            monster.AddAnimation(anim.Animation(Mushroom.Animations.Idle), animRes);
            monster.AddAnimation(anim.Animation(Mushroom.Animations.Run), animRes);
            monster.AddAnimation(anim.Animation(Mushroom.Animations.Death), animRes);
            monster.PlayAnimation(anim.Animation(Mushroom.Animations.Run));
            monster.SetRotation(new Quaternion(0f, 180f, 0f, 0f));
            monster.SetBounds(minBound, maxBound);

            monster.SetDirection(GetDirectionVector(0.03f), random.Next(400, 800));

            spawnMonster++;

            if (spawnMonster >= 25)
            {
                spawnMonster = 0;
                hpMonster++;
            }
        }

        /// <summary>
        /// Добавление в список IRandomMove
        /// </summary>
        public void AddRandomMove(IRandomMove obj)
        {
            monsterList.Add(obj);

            if (monsterList.Count >= MAX_SIZE)
            {
                GameManager.GoToScene(GameData.Scene.MenuScene);
            }
        }

        /// <summary>
        /// Удаление из списка IRandomMove при удалении этого объекта
        /// </summary>
        public void RemoveRandomMove(IRandomMove obj)
        {
            if (monsterList.Contains(obj))
            {
                monsterList.Remove(obj);
            }
        }

        public void AddClicker(IClick obj)
        {
            clickerList.Add(obj);
        }

        public void RemoveClicker(IClick obj)
        {
            clickerList.Remove(obj);
        }

        /// <summary>
        /// Проверка статуса движения IRandomMove
        /// </summary>
        public void UpdateRandomMove()
        {
            foreach(IRandomMove obj in monsterList)
            {
                if (obj.MoveTo())
                {
                    obj.SetDirection(GetDirectionVector(0.03f), random.Next(400, 800));
                }
            }
        }

        public void ChangeAnimation(Monster obj, int state)
        {
            if (state == 2)
            {
                obj.PlayAnimation(anim.Animation(Mushroom.Animations.Idle));
            }
            else if (state == 1)
            {
                obj.PlayAnimation(anim.Animation(Mushroom.Animations.Death));
            }
        }

        private void UpdateSpawn()
        {
            if (timeSpawn-- <= 0)
            {
                fullTimeSpawn = Mathf.RoundToInt(fullTimeSpawn * 0.99f);
                timeSpawn = fullTimeSpawn;

                AddMonster();
            }
        }

        public void UpdateClicker()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Stationary)
                {
                    foreach (IClick obj in clickerList)
                    {
                        obj.Click(touch.position);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                foreach (IClick obj in clickerList)
                {
                    obj.Click(Input.mousePosition);
                }
            }
        }

        private void UpdateCheckQuit()
        {
            if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
            {
                GameManager.GoToScene(GameData.Scene.MenuScene);
            }
        }

        private Vector3 GetDirectionVector(float speed)
        {
            float x = (float)random.NextDouble() * speed;
            float y = Mathf.Sqrt(Mathf.Pow(speed, 2f) - Mathf.Pow(x, 2f));

            int dirX = random.Next(0, 1);
            int dirY = random.Next(0, 1);

            if (dirX == 0)
            {
                dirX = -1;
            }

            if (dirY == 0)
            {
                dirY = -1;
            }

            return new Vector3(x * dirX, y * dirY);
        }

        private Vector3 GetStartPosition()
        {
            float rangeX = (float)random.NextDouble() * (maxBound.x - minBound.x) + minBound.x;
            float rangeY = (float)random.NextDouble() * (maxBound.y - minBound.y) + minBound.y;

            Debug.Log("NIKI = " + GameData.WorldToScreen(new Vector3(rangeX, rangeY, 4f)));
            return new Vector3(rangeX, rangeY, 4f);
        }
    }
}
