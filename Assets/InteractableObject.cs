using UnityEngine;
using GameEnums;
using YourNamespaceName;

public class InteractableObject : MonoBehaviour
{
    public PowerUpTypeEnum powerUpType;

    public void Interact(PlayerController player)
    {
        HandlePowerUp(player);

        // Implement any additional behavior specific to this interactable object.
        // For example, you may deactivate the object after interaction.
        gameObject.SetActive(false);
    }

    private void HandlePowerUp(PlayerController player)
    {
        switch (powerUpType)
        {
            case PowerUpTypeEnum.FakeInvincibility:
                player.TakeDamage(player.GetCurrentHealth() / 2); // Damages player to 50% health.
                break;
            case PowerUpTypeEnum.Revive:
                player.ReviveInCurrentStage(true, 1f); // Revive with full health at the current position.
                break;
            case PowerUpTypeEnum.FakeRevive:
                player.ReviveInCurrentStage(false, 0.2f); // Revive with 20% health at the current stage's start position.
                break;
            case PowerUpTypeEnum.Invincibility:
                player.StartCoroutine(player.ActivateInvincibility()); // Activate Invincibility for 5 seconds.
                break;
            case PowerUpTypeEnum.MeteorResistance:
                player.ActivateMeteorResistance(10f); // Activate Meteor Resistance for 10 seconds.
                break;
            case PowerUpTypeEnum.FakeMeteorResistance:
                player.ActivateFakeMeteorResistance(); // Activate Fake Meteor Resistance.
                break;
            case PowerUpTypeEnum.Teleportation:
                player.TeleportToNextStage(); // Teleport to the next stage.
                break;
            case PowerUpTypeEnum.FakeTeleportation:
                player.FakeTeleportationBacktoBeginning(); // Teleport back to the beginning of the current stage.
                break;
            case PowerUpTypeEnum.Sprint:
                player.ActivateSprint(8f); // Increase movement speed by 25% for 10 seconds.
                break;
            case PowerUpTypeEnum.FakeSprint:
                player.ActivateFakeSprint(); // Decrease movement speed by 35% for 15 seconds.
                break;
            case PowerUpTypeEnum.FakeHealthKit:
                player.ActivateFakeHealthKit(); // Decrease health by 5% per 5 seconds for 15 seconds.
                break;
            default:
                break;
        }
        gameObject.SetActive(false);
    }
}

