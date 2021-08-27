using Patterns;
using UnityEngine;

namespace Clicker
{
    internal class Monster : MonoBehaviour, IRandomMove, IClick
    {
        Transform objTransform;

        GameObject model;
        Animation anim;

        Vector3 direction;
        Vector2 minCorner;
        Vector2 maxCorner;
        int stepMove;

        int hp;
        bool stayToDeath = false;

        private void Awake()
        {
            PlayController.Instance.AddRandomMove(this);
            PlayController.Instance.AddClicker(this);
        }

        public void InstanceDestroy()
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        public void OnDestroy()
        {
            PlayController.Instance.RemoveRandomMove(this);
            PlayController.Instance.RemoveClicker(this);
        }

        public void AddModel(string name, string path, Vector3 position, int hp)
        {
            objTransform = gameObject.transform;
            objTransform.position = position;

            model = Instantiate(Resources.Load(path + name)) as GameObject;
            model.name = name;
            model.transform.SetParent(gameObject.transform);

            this.hp = hp;
        }

        public void SetRotation(Quaternion rotate)
        {
            if (objTransform != null)
            {
                objTransform.rotation = rotate;
            }
        }

        public void AddAnimation(string name, string path)
        {
            AnimationClip clip = Resources.Load<AnimationClip>(path + name);

            if (anim == null)
            {
                anim = model.AddComponent<Animation>();
            }

            anim.AddClip(clip, name);
        }

        public void PlayAnimation(string name)
        {
            if (anim != null)
            {
                anim.Play(name);
            }    
        }

        public void AddSkin(string name, string path)
        {
            Material skinMaterial = Resources.Load<Material>(path + name);

            SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
            skin.material = skinMaterial;
        }

        public void ScaleModel(Vector3 scaling)
        {
            if (objTransform != null)
            {
                objTransform.localScale = scaling;
            }
        }

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

            objTransform.position += direction;
            stepMove--;

            if (minCorner != null && maxCorner != null)
            {
                float x = objTransform.position.x;
                float y = objTransform.position.y;

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

        public void Click(Vector2 pos)
        {
            Vector3 cur = GameData.WorldToScreen(objTransform.position);
            Vector2 posStr = new Vector2(pos.x - 60f, pos.y - 80f);
            Vector2 posEnd = new Vector2(pos.x + 60f, pos.y + 80f);

            Debug.Log("Click: pos=" + pos.ToString() + "  cur=" + cur.ToString());

            if (cur.x > posStr.x &&
                cur.x < posEnd.x &&
                cur.y > posStr.y &&
                cur.y < posEnd.y)
            {
                hp--;

                PlayController.Instance.ChangeAnimation(this, hp);

                if (hp == 0)
                {
                    InstanceDestroy();
                }
                else if (hp == 1)
                {
                    stayToDeath = true;
                }
            }
        }
    }
}
