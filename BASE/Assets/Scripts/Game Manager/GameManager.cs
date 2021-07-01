using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class GameManager : MonoSingleton<GameManager>
    {
        //==================================================
        // Fields
        //==================================================

        [Space]
        [SerializeField] private List<ColorBlock> _blocks;
        [SerializeField] LevelBuilder _levelBuilder;
        [SerializeField] Factory _factory;
        [SerializeField] private RectTransform _dragField;
        [SerializeField] private EffectsPanel _effects;

        private ColorBlock _leftBlock;
        private ColorBlock _rightBlock;

        //==================================================
        // Properties
        //==================================================

        public List<FactoryPartsBlock> FactoryBlocks { get; private set; }
        public RectTransform DragField { get { return _dragField; } }
        public Robot LeftRobot { get; set; }
        public Robot RightRobot { get; set; }
        public EffectsPanel Effests { get { return _effects; } }

        //==================================================
        // Methods
        //==================================================

        protected override void Init()
        {
            base.Init();

            FactoryPartsBlock.ClearEvents();
            DragElement.ClearEvents();

            List<ColorBlock> blocks = new List<ColorBlock>(_blocks);
            _leftBlock = ExtractRandomColorBlock(blocks);
            _rightBlock = ExtractRandomColorBlock(blocks);

            _levelBuilder.BuildLevel(_leftBlock, Orientations.Left);
            _levelBuilder.BuildLevel(_rightBlock, Orientations.Right);

            this.FactoryBlocks = new List<FactoryPartsBlock>();
            _factory.LoadFactoryItems(_leftBlock, _rightBlock);
                        
            FactoryPartsBlock.OnBlockIsEmpty += OnBlockIsEmpty;

            if (this.FactoryBlocks.Count > 0)
            {
                const int NEAREST_BLOCK = 0;
                this.FactoryBlocks[NEAREST_BLOCK].Activate();
                _factory.MoveToNextBlock();
            }

            this.LeftRobot.Initialize();
            this.RightRobot.Initialize();
        }

        private ColorBlock ExtractRandomColorBlock(List<ColorBlock> blocks)
        {            
            int index = Random.Range(0, blocks.Count);
            ColorBlock target = blocks[index];
            blocks.RemoveAt(index);

            return target;
        }

        #region Events
        private void OnBlockIsEmpty()
        {
            const int NEAREST_BLOCK = 0;

            if (this.FactoryBlocks.Count > 0)
                this.FactoryBlocks.RemoveAt(NEAREST_BLOCK);

            if (this.FactoryBlocks.Count > 0)
            {
                this.FactoryBlocks[NEAREST_BLOCK].Activate();
                _factory.MoveToNextBlock();
            }

        }
        #endregion
    }
}