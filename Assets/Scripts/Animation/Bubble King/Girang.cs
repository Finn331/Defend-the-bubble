using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girang : MonoBehaviour
{
    [Header("Position Setting")]
    [SerializeField] private float posX;      // Tidak digunakan saat ini
    [SerializeField] private float posY;      // Posisi target pertama
    [SerializeField] private float secPosY;   // Posisi target kedua

    [Header("Gameobject Setting")]
    [SerializeField] private GameObject bubbleKing; // Objek yang akan dianimasikan

    [Header("Animation Setting")]
    [SerializeField] private float duration = 0.5f; // Durasi animasi per transisi

    // Update is called once per frame
    void Update()
    {
        if (bubbleKing == null) return; // Validasi null check untuk mencegah error
    }

    private void OnEnable()
    {
        StartJumpingLoop();
    }

    private void StartJumpingLoop()
    {
        if (bubbleKing == null) return;

        // Animasi melompat ke atas lalu ke bawah dengan LeanTween
        LeanTween.moveLocalY(bubbleKing, posY, duration)
            .setEaseOutBack()
            .setOnComplete(() =>
            {
                LeanTween.moveLocalY(bubbleKing, secPosY, duration)
                    .setEaseOutBack()
                    .setOnComplete(() =>
                    {
                        StartJumpingLoop(); // Looping berlanjut
                    });
            });
    }
}
