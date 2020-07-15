using System.Collections.Generic;
using System.Linq;

namespace TopDownShooter
{
    public class UIManager : SingletonGameObject<UIManager>
    {
        private List<UIPanel> _panels = null;

        protected override void Awake()
        {
            base.Awake();

            _panels = GetComponentsInChildren<UIPanel>(true).ToList();
        }

        private void Start()
        {
            ShowPanel(UIPanelType.Gameplay);
        }

        public void ShowPanel(UIPanelType type)
        {
            foreach (var panel in _panels)
            {
                panel.gameObject.SetActive(panel.Type == type);
            }
        }
    }
}
