using Patterns;
using UnityEngine;

namespace Clicker
{
    internal class Monster : MonoBehaviour, IRandomMove, IClick
    {
        bool active;
        float hpStart;
        float hp;

        bool stayToDeath;

        Vector2 minCorner;
        Vector2 maxCorner;
        Vector3 direction;
        int stepMove;

        Transform mTransform;
        Animation mAnimation;
        SkinnedMeshRenderer mSkin;


        // 1. MONOBEHAVIOUR

        private void Awake()
        {
            PlayController.Instance.AddRandomMove(this);
            PlayController.Instance.AddClicker(this);
        }

        public void OnDestroy()
        {
            PlayController.Instance.RemoveRandomMove(this);
            PlayController.Instance.RemoveClicker(this);
        }

        public void InstanceDestroy()
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }


        // 2. TRANSFORM

        public void ScaleModel(Vector3 scaling)
        {
            if (mTransform != null)
            {
                mTransform.localScale = scaling;
            }
        }

        public void SetRotation(Quaternion rotate)
        {
            if (mTransform != null)
            {
                mTransform.rotation = rotate;
            }
        }


        // 3. ANIMATION

        public void AddAnimation(string name, string path)
        {
            if (mAnimation != null)
            {
                AnimationClip clip = Resources.Load<AnimationClip>(path + name);
                mAnimation.AddClip(clip, name);
            }
        }

        public void PlayAnimation(string name)
        {
            if (mAnimation != null)
            {
                mAnimation.Play(name);
            }
        }

        public void AddSkin(string name, string path)
        {
            if (mSkin != null)
            {
                mSkin.material = Resources.Load<Material>(path + name);
            }
        }


        // 4. CLICK

        public float Health
        {
            get
            {
                return hp;
            }
        }

        public void InitHealth(float hpValue)
        {
            if (hp == 0f)
            {
                hp = hpValue;
                hpStart = hp;
                mTransform = gameObject.transform;
                mAnimation = gameObject.AddComponent<Animation>();
                mSkin = GetComponentInChildren<SkinnedMeshRenderer>();
                active = true;
            }
        }

        public void Click(Vector2 pos, float maxDamage, float damageRadius)
        {
            if (active)
            {
                Vector3 cur = GameData.WorldToScreen(mTransform.position);
                Debug.Log("Click: pos=" + pos.ToString() + "  cur=" + cur.ToString());

                float radius = Vector3.Distance(pos, cur);

                if (radius < damageRadius)
                {
                    hp -= (damageRadius - radius) / damageRadius * maxDamage;

                    if (hp <= 0f)
                    {
                        InstanceDestroy();
                    }
                    else
                    {
                        float hpRemains = hp / hpStart;
                        PlayController.Instance.ChangeAnimation(this, hpRemains);

                        if (hpRemains < 0.33f)
                        {
                            stayToDeath = true;
                        }
                    }
                }
            }
        }


        // 3. RANDOM MOVE

        public void SetDirection(Vector3 direction, int stepMove)
        {
            this.direction = direction;
            this.stepMove = stepMove;
        }

        public bool MoveTo()
        {
            if (stayToDeath)
            {
                return false;
            }

            mTransform.position += direction;
            stepMove--;

            if (minCorner != null && maxCorner != null)
            {
                float x = mTransform.position.x;
                float y = mTransform.position.y;

                if (x > maxCorner.x || x < minCorner.x)
                {
                    direction = new Vector3(direction.x * -1f, direction.y);
                }

                if (y > maxCorner.y || y < minCorner.y)
                {
                    direction = new Vector3(direction.x, direction.y * -1f);
                }
            }

            if (stepMove <= 0)
            {
                return true;
            }

            return false;
        }

        public void SetBounds(Vector2 minCorner, Vector2 maxCorner)
        {
            this.minCorner = minCorner;
            this.maxCorner = maxCorner;
        }
    }
}
