﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float speed = 10;
    public float smooth = 1;
    public float direction_x = 1f;
    public float direction_y = 1f;
    public Player player1;
    public Player player2;
    public Game game;
    float _x;
    float _y;

    void Update () {
        if (game.type == Game.types.INTRO) {
            if (transform.localScale.x == 0.700f) {
                transform.localScale = new Vector2 (transform.localScale.x - 0.700f, transform.localScale.y - 0.700f);
            }
            return;
        }
        if (transform.localScale.x == 0f) {
            transform.localScale = new Vector2 (transform.localScale.x + 0.700f, transform.localScale.y + 0.700f);
        }
        _x = transform.position.x + speed * direction_x * Time.deltaTime;
        _y = transform.position.y + speed * direction_y * Time.deltaTime;
        if (_x >= game.sizes.x) {
            game.Win (2, 10);
            Reset ();
        } else if (_x <= -game.sizes.x) {
            game.Win (1, 10);
            Reset ();
        }
        if (_y >= game.sizes.y)
            ChangeYDirection (1);
        else if (_y <= -game.sizes.y)
            ChangeYDirection (-1);
        transform.position = new Vector2 (_x, _y);
    }

    private void Reset () {
        int rand = Random.Range (0, 100);
        if (rand > 50)
            direction_x = 1;
        else
            direction_x = -1;
        rand = Random.Range (0, 100);
        if (rand > 50)
            direction_y = 1;
        else
            direction_y = -1;
        _x = _y = 0;
        transform.position = Vector3.zero;
        SpeedNormal();
    }

    private void ChangeYDirection (int direction) {
        _y = game.sizes.y * direction;
        direction_y *= -1;
    }

    public void AumentarSpeed () {
        speed = 27;
        CancelInvoke ();
        Invoke ("SpeedNormal", 0.300f);
    }

    void SpeedNormal () {
        speed = 11;
    }
}