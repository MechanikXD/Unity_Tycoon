using System.Collections.Generic;
using Core.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class ShopView : CanvasView
    {
        [SerializeField] private Image _sellImage;
        [SerializeField] private TMP_Dropdown _sellSelector;
        [SerializeField] private TMP_InputField _sellInput;
        [Space]
        [SerializeField] private Image _buyImage;
        [SerializeField] private TMP_InputField _buyInput;
        [SerializeField] private TMP_Dropdown _buySelector;
        [Space]
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _exchangeButton;

        [SerializeField] private Sprite _goldImage;
        [SerializeField] private Sprite _woodImage;
        [SerializeField] private Sprite _stoneImage;
        [SerializeField] private Sprite _oreImage;
        
        private readonly Dictionary<(ResourceType from, ResourceType to), float> _exchangePolicy = 
            new Dictionary<(ResourceType from, ResourceType to), float> 
            {
            [(ResourceType.Gold, ResourceType.Wood)] = 100,
            [(ResourceType.Gold, ResourceType.Stone)] = 500,
            [(ResourceType.Gold, ResourceType.Ore)] = 2000,
            [(ResourceType.Wood, ResourceType.Gold)] = 1,
            [(ResourceType.Wood, ResourceType.Stone)] = 50,
            [(ResourceType.Wood, ResourceType.Ore)] = 300,
            [(ResourceType.Stone, ResourceType.Gold)] = 0.1f,
            [(ResourceType.Stone, ResourceType.Wood)] = 0.5f,
            [(ResourceType.Stone, ResourceType.Ore)] = 50,
            [(ResourceType.Ore, ResourceType.Gold)] = 0.01f,
            [(ResourceType.Ore, ResourceType.Wood)] = 0.05f,
            [(ResourceType.Ore, ResourceType.Stone)] = 0.1f,
        };

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(UIManager.Instance.ExitLastCanvas);
            _exchangeButton.onClick.AddListener(Exchange);

            _sellSelector.onValueChanged.AddListener(OnSellItemEntered);
            _buySelector.onValueChanged.AddListener(OnBuyItemEntered);
            _sellInput.onEndEdit.AddListener(OnSellAmountEntered);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(UIManager.Instance.ExitLastCanvas);
            _exchangeButton.onClick.RemoveListener(Exchange);

            _sellSelector.onValueChanged.RemoveListener(OnSellItemEntered);
            _buySelector.onValueChanged.RemoveListener(OnBuyItemEntered);
            _sellInput.onEndEdit.RemoveListener(OnSellAmountEntered);
        }

        private void SetDefault()
        {
            _sellImage.sprite = _woodImage;
            _buyImage.sprite = _goldImage;
            _sellSelector.SetValueWithoutNotify(1);
            _buySelector.SetValueWithoutNotify(0);
            _sellInput.SetTextWithoutNotify("1");
            _buyInput.SetTextWithoutNotify("1");
        }

        private void OnSellItemEntered(int newValue)
        {
            var sell = TextToResource(_sellSelector.options[_sellSelector.value].text);
            var buy = TextToResource(_buySelector.options[_buySelector.value].text);
            if (sell == buy)
            {
                if (buy == ResourceType.Gold)
                {
                    _buySelector.SetValueWithoutNotify(1);  // 1 is wood (probably)
                    buy = ResourceType.Wood;
                }
                else
                {
                    _buySelector.SetValueWithoutNotify(0);  // 0 is gold (probably)
                    buy = ResourceType.Gold;
                }
            }
            SetDefaultPriceFor(sell, buy);

            SetIcon(sell, _sellImage);
            SetIcon(buy, _buyImage);
        }

        private void OnBuyItemEntered(int newValue)
        {
            var sell = TextToResource(_sellSelector.options[_sellSelector.value].text);
            var buy = TextToResource(_buySelector.options[_buySelector.value].text);
            if (sell == buy)
            {
                if (sell == ResourceType.Gold)
                {
                    _sellSelector.SetValueWithoutNotify(1);  // 1 is wood (probably)
                    sell = ResourceType.Wood;
                }
                else
                {
                    _sellSelector.SetValueWithoutNotify(0);  // 0 is gold (probably)
                    sell = ResourceType.Gold;
                }
            }
            
            SetDefaultPriceFor(sell, buy);
            
            SetIcon(sell, _sellImage);
            SetIcon(buy, _buyImage);
        }

        private void SetDefaultPriceFor(ResourceType sell, ResourceType buy)
        {
            var policy = _exchangePolicy[(sell, buy)];
            if (policy < 1)
            {
                _sellInput.SetTextWithoutNotify(((int)(1f / policy)).ToString());
                _buyInput.SetTextWithoutNotify("1");
            }
            else
            {
                _sellInput.SetTextWithoutNotify(((int)policy).ToString());
                _buyInput.SetTextWithoutNotify("1");
            }
        }

        private void OnSellAmountEntered(string newAmount)
        {
            var sell = TextToResource(_sellSelector.options[_sellSelector.value].text);
            var buy = TextToResource(_buySelector.options[_buySelector.value].text);
            if (int.TryParse(newAmount, out var amount))
            {
                var realAmount = Mathf.Clamp(amount, 0, ResourceManager.Instance.Current.Get(sell));
                _sellInput.SetTextWithoutNotify(realAmount.ToString());
                
                var policy = _exchangePolicy[(sell, buy)];
                _buyInput.SetTextWithoutNotify(policy < 1
                    ? ((int)(realAmount * policy)).ToString()
                    : ((int)(realAmount / policy)).ToString());
            }
            else
            {
                SetDefaultPriceFor(sell, buy);
            }
        }

        private ResourceType TextToResource(string text)
        {
            return text switch
            {
                "Gold" => ResourceType.Gold,
                "Wood" => ResourceType.Wood,
                "Stone" => ResourceType.Stone,
                "Ore" => ResourceType.Ore,
                
                _ => ResourceType.Gold
            };
        }

        private void Exchange()
        {
            if (!int.TryParse(_sellInput.text, out var sellAmount)) return;
            if (!int.TryParse(_buyInput.text, out var buyAmount)) return;
            var sellType = TextToResource(_sellSelector.options[_sellSelector.value].text);
            var buyType = TextToResource(_buySelector.options[_buySelector.value].text);

            var selling = new ResourceBundle()
            {
                Gold = sellType == ResourceType.Gold ? sellAmount : 0,
                Wood = sellType == ResourceType.Wood ? sellAmount : 0,
                Stone = sellType == ResourceType.Stone ? sellAmount : 0,
                Ore = sellType == ResourceType.Ore ? sellAmount : 0,
            };
            var buying = new ResourceBundle()
            {
                Gold = buyType == ResourceType.Gold ? buyAmount : 0,
                Wood = buyType == ResourceType.Wood ? buyAmount : 0,
                Stone = buyType == ResourceType.Stone ? buyAmount : 0,
                Ore = buyType == ResourceType.Ore ? buyAmount : 0,
            };
            
            ResourceManager.Instance.Spend(selling);
            ResourceManager.Instance.AddResources(buying);
        }

        private void SetIcon(ResourceType ofType, Image icon)
        {
            icon.sprite = ofType switch
            {
                ResourceType.Gold => _goldImage,
                ResourceType.Wood => _woodImage,
                ResourceType.Stone => _stoneImage,
                ResourceType.Ore => _oreImage,
                _ => icon.sprite
            };
        }
        
        public override void Show()
        {
            SetDefault();
            _thisCanvas.enabled = true;
        }

        public override void Hide()
        {
            _thisCanvas.enabled = false;
        }
    }
}