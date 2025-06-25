using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace IAP
{
    [RequireComponent(typeof(Button))]
    public sealed class PurchaseButton : MonoBehaviour
    {
        [SerializeField] private string _productName = "";
        [SerializeField] private Text _priceText = null;

        [SerializeField] private UnityEvent _onSucces = null;
        [SerializeField] private UnityEvent _onFailed = null;

        private ProductInfo _productInfo = null;
        private List<string> _allNames;

        public Action SuccessAction { get; set; }

        private void Awake()
        {
            Button button = GetComponent<Button>();

            button.onClick.AddListener(() =>
            {
                PurchaseManager.Buy(IAPSettings.Data.GetProductInfo(_productName));
            });

            _allNames = new List<string>();
            
            _allNames.AddRange(IAPSettings.Data.GetAllProductInfos().Select(p => p.Name));
        }

        private void OnEnable()
        {
            UpdateProductInfo();
            
            if (_priceText != null)
            {
                _priceText.text = PurchaseManager.GetLocalizedPrice(_productInfo);
            }

            PurchaseManager.OnPurchaseSuccess += TryCallSucces;
            PurchaseManager.OnPurchaseFailed += TryCallFailed;

            if (PurchaseManager.IsProductPurchased(_productInfo))
            {
                gameObject.SetActive(false);
            }
        }

        public void UpdateProductInfo(int index)
        {
            _productName = _allNames[index];
            
            UpdateProductInfo();
        }

        private void OnDisable()
        {
            PurchaseManager.OnPurchaseSuccess -= TryCallSucces;
            PurchaseManager.OnPurchaseFailed -= TryCallFailed;
        }

        private void UpdateProductInfo()
        {
            _productInfo = IAPSettings.Data.GetProductInfo(_productName);
        }

        private void TryCallSucces(ProductInfo productInfo)
        {
            if (_productInfo.Name == productInfo.Name)
            {
                _onSucces?.Invoke();
                SuccessAction?.Invoke();

                if (_productInfo.Type == ProductType.NonConsumable)
                    gameObject.SetActive(false);
            }
        }

        private void TryCallFailed(ProductInfo productInfo, PurchaseFailureReason error)
        {
            if (_productInfo.Name == productInfo.Name)
            {
                _onFailed?.Invoke();
            }
        }
    }
}