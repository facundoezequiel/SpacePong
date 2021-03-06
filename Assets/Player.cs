﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float angleDistortion = 1;
    public float speed = 0.8f;
    public Game game;
    public types type;
    public enum types {
        PLAYER1,
        PLAYER2
    }
    float offset = 0.7f;
    public AudioSource audioSource;
    public Ball ball;

    void Update () {
        if (game.type == Game.types.INTRO) {
            return;
        }
        float player_y = transform.position.y;
        if (type == types.PLAYER1) {
            if (Input.GetKey (KeyCode.UpArrow) && player_y <= game.sizes.y - offset) {
                Move_y (1);
            }
            if (Input.GetKey (KeyCode.DownArrow) && player_y >= -game.sizes.y + offset) {
                Move_y (-1);
            }
        }

        if (type == types.PLAYER2) {
            if (Input.GetKey (KeyCode.W) && player_y <= game.sizes.y - offset) {
                Move_y (1);
            }
            if (Input.GetKey (KeyCode.S) && player_y >= -game.sizes.y + offset) {
                Move_y (-1);
            }
        }

        float player_x = transform.position.x;
        if (type == types.PLAYER2) {
            if (transform.position.x < -1) {
                if (Input.GetKey (KeyCode.D) && player_x <= game.sizes.x - offset) {
                    Move_x (1);
                }
            }
            if (Input.GetKey (KeyCode.A) && player_x >= -game.sizes.x + offset) {
                Move_x (-1);
            }
        }

        if (type == types.PLAYER1) {
            if (transform.position.x > 1) {
                if (Input.GetKey (KeyCode.LeftArrow) && player_x >= -game.sizes.x + offset) {
                    Move_x (-1);
                }
            }
            if (Input.GetKey (KeyCode.RightArrow) && player_x <= game.sizes.x - offset) {
                Move_x (1);
            }
        }
    }

    void Move_y (int direction) {
        transform.position = new Vector2 (transform.position.x, transform.position.y + speed * direction * Time.deltaTime);
    }

    void Move_x (int direction) {
        transform.position = new Vector2 (transform.position.x + speed * direction * Time.deltaTime, transform.position.y);
    }

    private void OnTriggerEnter (Collider other) {
        Ball ball = other.gameObject.GetComponent<Ball> ();
        if (ball == null)
            return;

        //choco con la pelota:
        game.playerActiveType = type;
        audioSource.Play ();
        ball.AumentarSpeed ();
        float center = transform.position.y;
        ball.direction_y += (ball.transform.position.y - center) * angleDistortion;
        if (ball.direction_y > 1.1f)
            ball.direction_y = 1.1f;
        else if (ball.direction_y < -1.1f)
            ball.direction_y = -1.1f;
        other.gameObject.GetComponent<Ball> ().direction_x *= -1;
    }

    public void AumentarTamaño () {
        if (transform.localScale.y == 2) { // Si el tamaño en Y es 2, osea el original
            transform.localScale = new Vector2 (transform.localScale.x + 0.250f, transform.localScale.y + 0.500f); // Aumentar tanto en X y tanto en Y
        }
        CancelInvoke (); // Cancelar el Invoke si habia uno
        Invoke ("TamañoNormal", 2f); // Empezar el Invoke o reiniciarlo para llegar a la funcion que vuelve al tamaño normal
    }

    public void TamañoNormal () {
        if (transform.localScale.y == 2.500f) { // Si el tamaño en Y es 3, osea esta aumentado
            transform.localScale = new Vector2 (transform.localScale.x - 0.250f, transform.localScale.y - 0.500f); // Restarle lo anteriormente establecido para volver al original
            // Continua en Coin
        }
    }
}