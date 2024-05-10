using System.Collections;
using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    public abstract string UIMessage();

    public virtual void ApplyPowerUp(PlayerController player, GameManager game)
    {
        game.Notify(UIMessage()); 
    }
}

public class MaxHealth : PowerUps
{
    public override string UIMessage()
    {
        return "MAX HEALTH";
    }

    public override void ApplyPowerUp(PlayerController player, GameManager game)
    {
        base.ApplyPowerUp(player, game);
        player.SetHealth(player.GetMaxHealth());
        player.GetHBar().SetValue(player.GetMaxHealth());
    }
}

public class MaxBoosts : PowerUps
{
    public override string UIMessage()
    {
        return "MAX BOOSTS";
    }

    public override void ApplyPowerUp(PlayerController player, GameManager game)
    {
        base.ApplyPowerUp(player, game);
        player.SetBoosts(player.GetMaxBoosts());
        player.GetSBar().SetValue(player.GetMaxBoosts());
    }
}

public class TimeSlow : PowerUps
{
    public override string UIMessage()
    {
        return "TIME SLOW";
    }

    public override void ApplyPowerUp(PlayerController player, GameManager game)
    {
        base.ApplyPowerUp(player, game);
        Time.timeScale = 0.5f; 
        game.StartCoroutineTimer(5.0f, 2, "");
    }
}

public class DeathMachine : PowerUps
{
    public override string UIMessage()
    {
        return "DEATH MACHINE";
    }

    public override void ApplyPowerUp(PlayerController player, GameManager game)
    {
        base.ApplyPowerUp(player, game);
        player.SetDelay(0.05f); 
        game.StartCoroutineTimer(5.0f, 3, "");
    }
}

public class DoublePoints : PowerUps
{
    public override string UIMessage()
    {
        return "DOUBLE POINTS";
    }

    public override void ApplyPowerUp(PlayerController player, GameManager game)
    {
        base.ApplyPowerUp(player, game);
        game.SetDoublePoints(true);
        game.StartCoroutineTimer(7.5f, 1, "");
    }
}

public class Laser : PowerUps
{
    public override string UIMessage()
    {
        return "LASER BEAM";
    }

    public override void ApplyPowerUp(PlayerController player, GameManager game)
    {
        base.ApplyPowerUp(player, game);
        //Laser functionality
    }
}

public class Nuclear : PowerUps
{
    public override string UIMessage()
    {
        return "NUCLEAR";
    }

    public override void ApplyPowerUp(PlayerController player, GameManager game)
    {
        base.ApplyPowerUp(player, game);
        
        EnemyController[] enemies = game.FindEnemies();
        foreach (EnemyController enemy in enemies)
        {
            if (game.IsDoublePoints()) {
                game.AddScore(enemy.reward * 2);
            } else {
                game.AddScore(enemy.reward);
            }

            game.DestroyGameObject(enemy.gameObject);
        }

        Asteroid[] asteroids = game.FindAsteroids();
        foreach (Asteroid asteroid in asteroids)
        {
            if (game.IsDoublePoints()) {
                game.AddScore(asteroid.reward * 2);
            } else {
                game.AddScore(asteroid.reward);
            }

            game.DestroyGameObject(asteroid.gameObject);
        }
    }
}

public class Invulnerability : PowerUps
{
    private HealthBar hBar;
    private HealthBar sBar;
    private PlayerController setPlayer;
    private float invunTimer = 3f;
    private float respawnTimer = 3f;

    public override string UIMessage()
    {
        return "INVINCIBLE";
    }

    public override void ApplyPowerUp(PlayerController player, GameManager game)
    {
        base.ApplyPowerUp(player, game);
        setPlayer = player;
        hBar = player.GetHBar();
        sBar = player.GetSBar();

        SetSpriteOpacity(0.5f);
        player.gameObject.layer = LayerMask.NameToLayer("Ignore");
        Invoke(nameof(TurnOnCollisions), invunTimer);

        float flashInterval = respawnTimer / 8;
        for (int i = 1; i < 8; i++)
        {
            float time = i * flashInterval;
            game.StartCoroutine(WaitAndToggleVisibility(time));
        }
        game.StartCoroutine(WaitAndToggleVisibility(respawnTimer));
        game.StartCoroutine(WaitAndSetSpriteFullOpacity(respawnTimer + 0.1f));
    }

    private IEnumerator WaitAndToggleVisibility(float time)
    {
        yield return new WaitForSeconds(time);
        ToggleVisibilityWithParam();
    }

    private IEnumerator WaitAndSetSpriteFullOpacity(float time)
    {
        yield return new WaitForSeconds(time);
        SetSpriteFullOpacity();
    }

    protected void SetSpriteFullOpacity()
    {
        SetSpriteOpacity(1f);
    }

    protected void SetSpriteOpacity(float opacity)
    {
        Color spriteColor = setPlayer.GetSpriteRenderer().color;
        spriteColor.a = opacity;
        setPlayer.GetSpriteRenderer().color = spriteColor;
    }

    protected void ToggleVisibilityWithParam()
    {
        ToggleVisibility(!IsVisible());
    }

    protected bool IsVisible()
    {
        Renderer[] renderers = setPlayer.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer renderer in renderers)
        {
            if (renderer.enabled)
            {
                return true;
            }
        }
        return false;
    }

    protected void ToggleVisibility(bool isVisible)
    {
        Renderer[] renderers = setPlayer.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isVisible;
        }
        hBar.gameObject.SetActive(isVisible);
        sBar.gameObject.SetActive(isVisible);
    }

    protected void TurnOnCollisions()
    {
        setPlayer.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}