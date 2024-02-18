using UnityEngine;

namespace Content.Core.ControlHintSystem
{
    public static class ControlHintSystem
    {
        private const int UnusedControlReminderThreshold = 3;

        private static bool _dashPromptObsolete;
        private static int _dashPromptLastLevel;
        private static int _abilityPromptLastLevel = -3;

        private static ControlHintManager _manager;

        static ControlHintSystem()
        {
            EventManager.OnMainMenuEnter.Subscribe(Reset);
            EventManager.OnLevelEnter.Subscribe(CheckForNecessaryControlReminder);
            EventManager.OnPlayerDash.Subscribe(() => _dashPromptLastLevel = LevelManager.levelCounter);
            EventManager.OnPlayerAbilityUsed.Subscribe(() =>
            {
                if (UpgradeManager.HasBindedAbility())
                {
                    _abilityPromptLastLevel = LevelManager.levelCounter;
                }
            });
        }

        public static void RegisterManager(ControlHintManager manager)
        {
            _manager = manager;
        }


        private static void Reset()
        {
            _dashPromptLastLevel = 0;
            _abilityPromptLastLevel = -3;

            _dashPromptObsolete = false;

            _manager.movementPrompt.SetActive(false);
            _manager.shootPrompt.SetActive(false);

            _manager.reloadPrompt.SetActive(false);
            _manager.dashPrompt.SetActive(false);
            _manager.abilityPrompt.SetActive(false);
        }

        private static void CheckForNecessaryControlReminder()
        {
            var currentLevel = LevelManager.levelCounter;

            if (currentLevel == 0)
            {
                return;
            }

            if (currentLevel == 1)
            {
                TriggerReloadPrompt();
                return;
            }

            if (currentLevel >= _abilityPromptLastLevel + UnusedControlReminderThreshold)
            {
                if (UpgradeManager.HasBindedAbility())
                {
                    _abilityPromptLastLevel = LevelManager.levelCounter;
                    _manager.DisplayPrompt(_manager.abilityPrompt);
                }
            }
            else if (currentLevel >= _dashPromptLastLevel + UnusedControlReminderThreshold)
            {
                _dashPromptLastLevel = LevelManager.levelCounter;
                _manager.DisplayPrompt(_manager.dashPrompt);
            }
        }

        public static void ShowBasicControlPrompt()
        {
            _manager.ShowPrompt(_manager.movementPrompt);
            _manager.ShowPrompt(_manager.shootPrompt);
        }

        public static void HideBasicControlPrompt()
        {
            _manager.HidePrompt(_manager.movementPrompt);
            _manager.HidePrompt(_manager.shootPrompt);
        }

        public static void TriggerReloadPrompt()
        {
            _manager.DisplayPrompt(_manager.reloadPrompt);
        }

        public static void TriggerDashPrompt()
        {
            if (!_dashPromptObsolete)
            {
                _manager.DisplayPrompt(_manager.dashPrompt, false);
                _dashPromptObsolete = true;
            }
        }
    }
}