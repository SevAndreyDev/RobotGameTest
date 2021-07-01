using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace EnglishKids.RobotGame
{
    public class Factory : MonoBehaviour
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] private RectTransform _targetMovableTransform;
        [SerializeField] private float _targetScreenHeight;
        [SerializeField] private float _topImageOffset;
        [SerializeField] private float _separateBlocksOffset;
        [SerializeField] private FactoryPartsBlock _factoryBlockPrefab;
        [SerializeField] private int _itemsInBlock = 5;
        [SerializeField] private float _movementDuration = 3f;
        [SerializeField] private FactoryItem _prefab;

        private AudioManager _audio;        

        //==================================================
        // Properties
        //==================================================

        //==================================================
        // Methods
        //==================================================
        
        public void LoadFactoryItems(ColorBlock leftBlock, ColorBlock rightBlock)        
        {
            if (_audio == null)
                _audio = AudioManager.Instance;

            List<ColorBlock.RobotPartConfig> list = new List<ColorBlock.RobotPartConfig>();
            FillList(list, leftBlock);
            FillList(list, rightBlock);

            int itemsCount = list.Count;
            int blocksCount = (itemsCount / _itemsInBlock);
            if (itemsCount % _itemsInBlock != 0)
                blocksCount++;

            FactoryPartsBlock startBlock = Instantiate(_factoryBlockPrefab, _targetMovableTransform);
            startBlock.CachedTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _topImageOffset);
            startBlock.gameObject.SetActive(true);

            startBlock = Instantiate(_factoryBlockPrefab, _targetMovableTransform);
            startBlock.CachedTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _targetScreenHeight);
            startBlock.gameObject.SetActive(true);
            
            for (int i = 0; i < blocksCount; i++)
            {
                FactoryPartsBlock block = Instantiate(_factoryBlockPrefab, _targetMovableTransform);
                float height = 0f;

                for (int j = 0; j < _itemsInBlock && list.Count > 0; j++)
                {
                    int index = Random.Range(0, list.Count);
                    height += AddFactoryItem(block.CachedTransform,  list[index]);
                    block.PartsCount++;
                    list.RemoveAt(index);                    
                }

                float screenHeight = _targetScreenHeight - _topImageOffset;

                height = height > screenHeight ? height : screenHeight;

                block.CachedTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
                block.gameObject.SetActive(true);

                GameManager.Instance.FactoryBlocks.Add(block);

                FactoryPartsBlock separateBlock = Instantiate(_factoryBlockPrefab, _targetMovableTransform);
                separateBlock.CachedTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _topImageOffset);
                separateBlock.gameObject.SetActive(true);
            }
        }

        public void MoveToNextBlock()
        {
            _audio.PlaySound(Audio.FactoryMoving);
            Vector3 position = _targetMovableTransform.localPosition;
            Vector3 target = position + Vector3.up * _targetScreenHeight;

            var secuance = DOTween.Sequence();
            secuance.Append(_targetMovableTransform.DOLocalMove(target, _movementDuration));//.SetEase(_filledEase);            

            secuance.OnComplete(() =>
            {
                _audio.PlaySound(Audio.FactoryMoving);
            });
        }

        private void FillList(List<ColorBlock.RobotPartConfig> list, ColorBlock colorBlock)
        {
            foreach (var item in colorBlock.RobotConfigurations)
            {
                item.ColorBlockSettings = colorBlock;
                //item.ScaleFactor = colorBlock.ScaleFactor;
                //item.ColorKind = colorBlock.Kind;
                if (item.robotPart != BodyParts.Shadow)
                {
                    list.Add(item);
                }
            }
        }

        private float AddFactoryItem(RectTransform parent, ColorBlock.RobotPartConfig configItem)
        {
            FactoryItem item = Instantiate(_prefab, parent);
            item.ActivateImage(configItem);

            return item.Height;
        }
    }
}