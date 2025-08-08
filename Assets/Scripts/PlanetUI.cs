using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetUI : BaseContadorUI
{
    [SerializeField] protected TMP_Text GoalText;
    [SerializeField] protected int goalPoints = 1000;

    [SerializeField] protected string upgradeNameToTrack = "Upgrade_1";
    [SerializeField] protected int goalLevel = 10;
    [SerializeField] protected TMP_Text upgradeGoalText;
    [SerializeField] protected TMP_Text GoalText2;
    [SerializeField] protected TMP_Text upgradeGoalText2;
    [SerializeField] protected int goalPoints2 = 5000;
    [SerializeField] protected string upgradeNameToTrack2 = "Upgrade_2";
    [SerializeField] protected int goalLevel2 = 20;
    [Header("Floating Text")]
    [SerializeField] protected Transform floatingTextContainer;
    [SerializeField] protected TMP_Text floatingTextPrefab;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        if (GoalText2 != null) GoalText2.gameObject.SetActive(false);
        if (upgradeGoalText2 != null) upgradeGoalText2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public void SpawnFloatingText(double amount)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0f);

        TMP_Text floatingText = Instantiate(floatingTextPrefab, floatingTextContainer);


        floatingText.rectTransform.anchoredPosition = randomOffset;


        floatingText.text = "+" + AbreviateNumber.Format(amount);
        floatingText.color = Color.green;


        Animator animator = floatingText.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("pop up");
        }

        Destroy(floatingText.gameObject, 1f);
    }
    public override void UpdateMinigameUI(int? cortes = null, int? troncos = null, float? tiempo = null, int? cortesNecesarios = null)
    {
        throw new System.NotImplementedException();
    }
    public override void OcultarMinigameTextos()
    {
        throw new System.NotImplementedException();
    }
    public override void OcultarMinigameTextos2()
    {
        throw new System.NotImplementedException();
    }     
    protected override IEnumerator ShowTemporaryText(TMP_Text text)
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateUI()
    {
        if (countText != null)
        {
            double currentPoints = GameManager.Instance.count;
            countText.text = AbreviateNumber.Format(currentPoints);

            if (GoalText != null)
            {
                GoalText.text = "Puntos: " + AbreviateNumber.Format(currentPoints) + " / " + AbreviateNumber.Format(goalPoints);

                if (currentPoints >= goalPoints)
                {
                    GoalText.color = Color.green;
                }
                else
                {
                    GoalText.color = Color.white;
                }
            }
        }
        if (gemsText != null)
        {
            double currentPoints = GameManager.Instance.gems;
            gemsText.text = AbreviateNumber.Format(currentPoints);
        }


        if (!string.IsNullOrEmpty(upgradeNameToTrack) && upgradeGoalText != null)
        {
            int currentLevel = PlayerPrefs.GetInt(upgradeNameToTrack + "_Level", 0);
            upgradeGoalText.text = "Mejora Arboles: " + upgradeNameToTrack + " nivel " + currentLevel + " / " + goalLevel;

            if (currentLevel >= goalLevel)
            {
                upgradeGoalText.color = Color.green;
            }
            else
            {
                upgradeGoalText.color = Color.white;
            }
        }
        bool firstGoalCompleted = false;
        bool firstUpgradeCompleted = false;

        if (GoalText != null && GoalText.color == Color.green)
            firstGoalCompleted = true;

        if (upgradeGoalText != null && upgradeGoalText.color == Color.green)
            firstUpgradeCompleted = true;

        if (firstGoalCompleted && firstUpgradeCompleted)
        {

            if (GoalText != null) GoalText.gameObject.SetActive(false);
            if (upgradeGoalText != null) upgradeGoalText.gameObject.SetActive(false);


            if (GoalText2 != null)
            {
                GoalText2.gameObject.SetActive(true);
                int currentPoints = Mathf.RoundToInt(GameManager.Instance.count);
                GoalText2.text = AbreviateNumber.Format(currentPoints) + " / " + AbreviateNumber.Format(goalPoints2);
                GoalText2.color = (currentPoints >= goalPoints2) ? Color.green : Color.white;
            }

            if (upgradeGoalText2 != null)
            {
                upgradeGoalText2.gameObject.SetActive(true);
                int currentLevel2 = PlayerPrefs.GetInt(upgradeNameToTrack2 + "_Level", 0);
                upgradeGoalText2.text = "Mejora Árboles: " + upgradeNameToTrack2 + " nivel " + currentLevel2 + " / " + goalLevel2;
                upgradeGoalText2.color = (currentLevel2 >= goalLevel2) ? Color.green : Color.white;
            }
        }
    }
}
