using UnityEngine;

internal class CursorHoverHightlighter
{
    public CursorHoverHightlighter()
    {
    }

    public void  Update(CustomCursor cursor)
    {
        var target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        var enemies = GameObject.FindGameObjectsWithTag(cursor.EnemyTag);
        bool isOverEnemy = false;

        foreach (var enemy in enemies)
        {
            var collider = enemy.GetComponent<SphereCollider>() as SphereCollider;
            if (collider.bounds.Contains(target))
            {
                isOverEnemy = true;

                if (cursor.GetStyle() != CustomCursor.CursorStyle.Attack)
                {
                    cursor.SetAttack(false);
                }
            }
        }

        if (!isOverEnemy && cursor.GetStyle() == CustomCursor.CursorStyle.Attack && !cursor.AttackModeWasSetByUser())
        {
            cursor.SetDefault(false);
        }
        
    }
}