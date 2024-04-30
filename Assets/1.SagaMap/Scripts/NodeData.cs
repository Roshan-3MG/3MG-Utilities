using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using ThreeMG.Helper.SagaMapManagement;
using TMPro;
using UnityEngine;

public class NodeData : NodeItem
{
    public TextMeshProUGUI levelID;
    public UnityEngine.UI.Image nodeIcon;
    public UnityEngine.UI.Button nodeButton;

    // Start is called before the first frame update
    void Start()
    {
        nodeButton.onClick.AddListener(() => OnButtonClick());

        levelID.text = nodeIndex.ToString();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        nodeIcon.sprite = GetSprite(levelType);
    }

    private Sprite GetSprite(LevelType levelType)
    {
        foreach (NodeClass nodeClass in SagaMapManager.Instance.nodeClasses)
        {
            if (nodeClass.levelType == levelType)
            {
                return nodeClass.levelSprite;
            }
        }

        return null;
    }

    private void OnButtonClick()
    {
        SagaMapManager.Instance.UpdateNode(NodeState.UNLOCKED);
        SagaMapManager.Instance.AnimateNodeProgression();
    }
}
