﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public enum TreeType : byte { FRUIT, SUPPORT }; //enum for the 2 tree types

    public TreeType type;           //to determine the type of the tree

    private int treeTypeNumber;

    public float health = 100f;            //tree's health
    public float SeedCost { get; set; }       //cost to make the tree
    private float seedValue = 10f;      //value of the seed recieved when harvested
    private float scoreValue = 100f;     //value of the score received when harvested
    [SerializeField] protected float harvestTime = 5f;      //time to harvest
    [SerializeField] protected float harvestTimer = 0f;     //timer for the harveset

    protected bool harvestable = false;                     //to check whether the tree is harvestable or not

	// Use this for initialization
	void Start () {
        GameManager.gameStart = true;
        type = CheckTreeType();
        harvestTimer = harvestTime;
	}
	
	// Update is called once per frame
	void Update () {
        CheckHealth();
        switch (type)   //the function differs depending on the tree type (FRUIT or SUPPORT)
        {
            case (TreeType.FRUIT):
                BearFruit();
                break;
            case (TreeType.SUPPORT):
                TreeEffect();
                break;
        }
	}
    //check the health of the Tree
    protected void CheckHealth()
    {
        if (health <= 0)    //when health reaches 0, destroy the tree
        {
            GameManager.numberOfTrees--;
            Destroy(gameObject);
        }
    }

    //The main algorithm for harvestable tree (FRUIT tree)
    protected void BearFruit()
    {
        if (harvestTimer <= 0 && !harvestable)
        {
            harvestTimer = harvestTime;
        }
        else if (!harvestable)
        {
            harvestTimer -= Time.deltaTime;
            if (harvestTimer <= 0) 
            {
                harvestable = true;             //when timer runs out, set tree into harvestable
                GetComponent<SpriteRenderer>().material.SetColor("_Color",Color.black);
            }
        }
    }

    //The main algorithm for supporting tree (SUPPORT tree)
    protected void TreeEffect()
    {
        if(treeTypeNumber == 4)
        {
            GrapePower();
        }
        else
        {
            MaplePower();
        }
    }

    //Things what will happen when a FRUIT tree is harvested
    protected void Harvest()
    {
        if (harvestable)
        {
            //access gamemanager to set seed and score
           
            GameManager.score += scoreValue;
            GameManager.seed += seedValue;

            GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
            Debug.Log("Score: " + GameManager.score);
            Debug.Log("Seed: " + GameManager.seed);
            harvestable = false;                   //set back the harvestable to false
        }
    }

    private TreeType CheckTreeType()
    {
        if(treeTypeNumber < 3)
        {
            return TreeType.FRUIT;
        }
        else
        {
            return TreeType.SUPPORT;
        }
    }

    //Do Harvest when the tree is clicked by the mouse
    private void OnMouseDown()
    {
        Harvest();
    }

    private void GrapePower()
    {

    }

    private void MaplePower()
    {

    }

    public void InitialiseAttribute(float health,float seedValue,float scoreValue,float harvestTime,int treeTypeNumber)
    {
        this.treeTypeNumber = treeTypeNumber;
        this.health = health;
        this.seedValue = seedValue;
        this.scoreValue = scoreValue;
        this.harvestTime = harvestTime;
    }
}
