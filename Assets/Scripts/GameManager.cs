﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public TiledMap map;
    public float minDelay = .2f;
    public float maxDelay = 2f;
    public int maxCombo = 10;
    public Camera mainCamera;

    public int combo { get; set; }

    public float currentDelay
    {
        get
        {
            return Mathf.Max(maxDelay - (float)(combo - 1) * (1f / (float)maxCombo) * (float)(maxDelay - minDelay), minDelay);
        }
        private set { }
    }

    public float cooldown { get; private set; }

    void Start()
    {
        combo = 1;
        if (instance)
            Destroy(this);
        instance = this;
        cooldown = currentDelay;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            combo++;
            cooldown = currentDelay;
            foreach (Entity e in map.entities)
                e.OnAction();
        }
    }
}
