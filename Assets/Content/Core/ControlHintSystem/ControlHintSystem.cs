namespace Content.Core.ControlHintSystem
{
    public static class ControlHintSystem
    {
        private const float ReminderDelay = 2f;
        private const float ShootInstructionDelay = 0.5f;
        private const int UnusedControlReminderThreshold = 2;

        private static bool _shootPromptObsolete;
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
            _shootPromptObsolete = false;

            if (_manager == null)
            {
                return;
            }

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

            if (currentLevel > _abilityPromptLastLevel + UnusedControlReminderThreshold && UpgradeManager.HasBindedAbility())
            {
                _abilityPromptLastLevel = LevelManager.levelCounter;
                _manager.DisplayPrompt(_manager.abilityPrompt, ReminderDelay);
            }
            else if (currentLevel >= _dashPromptLastLevel + UnusedControlReminderThreshold)
            {
                _dashPromptLastLevel = LevelManager.levelCounter;
                _manager.DisplayPrompt(_manager.dashPrompt, ReminderDelay);
            }
        }

        public static void ShowMovementControlPrompt()
        {
            _manager.ShowPrompt(_manager.movementPrompt);
        }

        public static void HideMovementControlPrompt()
        {
            _manager.HidePrompt(_manager.movementPrompt);
        }

        public static void TriggerReloadPrompt()
        {
            _manager.DisplayPrompt(_manager.reloadPrompt, ReminderDelay);
        }

        public static void TriggerShootPrompt()
        {
            if (!_shootPromptObsolete)
            {
                _manager.DisplayPrompt(_manager.shootPrompt, ShootInstructionDelay);
                _shootPromptObsolete = true;
            }
        }

        public static void TriggerDashPrompt()
        {
            if (!_dashPromptObsolete)
            {
                _manager.DisplayPrompt(_manager.dashPrompt);
                _dashPromptObsolete = true;
            }
        }
    }
}