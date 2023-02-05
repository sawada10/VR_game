using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchController : MonoBehaviour
{   
    // Snitchの位置を記録
    public Vector3 SnitchPosition;
    // 動く範囲を決定
    private float xmin = -1250;
    private float xmax = -150;
    private float zmin = 710;
    private float zmax = 950;
    private float ymin = 160;
    private float ymax = 210;
    // 速度
    private float vel_x, vel_y, vel_z;
    // 動きをスニッチっぽくするために、ランダムな動きを入れる
    private float random_vel_x = 0f, random_vel_y = 0f, random_vel_z = 0f;
    // 動いてよい範囲を超えていないかどうか
    private int limitOver = 0;
    // ランダムな動きに用いる最高速度を決めておく
    private float max_random_x_vel = 100, max_random_y_vel = 100, max_random_z_vel = 100;
    // 動きに用いる最高速度を決めておく
    private float max_x_vel = 40, max_y_vel = 20, max_z_vel = 40;
    // forward <=> x, up <=> y, right <=> -z
    // 動く時間
    private float moveDuration = 4;
    private float[] moveDurations = new float[6] {0.5f, 0.7f, 0.8f, 1.0f, 1.3f, 1.5f};
    // 止まるときかどうか
    private int stopNow = 0;
    private float stopDuration;
    private float[] stopDurations = new float[5] {1f, 1.5f, 2f, 2.2f, 2.5f};

    // 動いている時間もしくは止まっている時間を記録する変数
    private float seconds = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        randomVelocityDeterminer();
        moveController();
    }

    void velocityDeterminer(){
        SnitchPosition = transform.position;
        // スニッチの位置に応じて速度を設定する。
        SnitchPosition = transform.position;
        // ランダムで選ぶ速度の配列を作っておく
        float[] x_vels = new float[5] {- max_x_vel, - max_x_vel / 2, 0, max_x_vel / 2, max_x_vel};
        float[] y_vels = new float[5] {- max_y_vel, - max_y_vel / 2, 0, max_y_vel / 2, max_y_vel};
        float[] z_vels = new float[5] {- max_z_vel, - max_z_vel / 2, 0, max_z_vel / 2, max_z_vel};

        // もし、止まる時間だったら、止まる。
        if (stopNow == 1){
            vel_x = 0;
            vel_y = 0;
            vel_z = 0;
        }
        // 止まる時間でなく、動いていい範囲内だったらランダムで速度を決める
        else if(SnitchPosition[0] > xmin && SnitchPosition[0] < xmax && SnitchPosition[1] > ymin && SnitchPosition[1] < ymax && SnitchPosition[2] > zmin && SnitchPosition[2] < zmax){
            limitOver = 0;
            System.Random rand = new System.Random();
            int ix = rand.Next(0, 6);
            int iy = rand.Next(0, 6);
            int iz = rand.Next(0, 6);
            vel_x = x_vels[ix];
            vel_y = y_vels[iy];
            vel_z = z_vels[iz];
        }
        // 動いていい範囲を超えていたら、元に戻す方向に動くように速度設定をする
        else {
            limitOver = 1;
            if(SnitchPosition[0] <= xmin){
                vel_x = max_x_vel;
            }
            if(SnitchPosition[0] >= xmax){
                vel_x = - max_x_vel;
            }
            if(SnitchPosition[1] <= ymin){
                vel_y = max_y_vel;
            }
            if(SnitchPosition[1] >= ymax){
                vel_y = - max_y_vel;
            }
            if(SnitchPosition[2] <= zmin) {
                vel_z = max_z_vel;
            }
            if(SnitchPosition[2] >= zmax){
                vel_z = - max_z_vel;
            }
        }
    }


    void randomVelocityDeterminer(){
        // スニッチの位置に応じて速度を設定する。
        SnitchPosition = transform.position;
        // ランダムで選ぶ速度の配列を作っておく
        float[] random_x_vels = new float[5] {- max_random_x_vel, - max_random_x_vel / 2, 0, max_random_x_vel / 2, max_random_x_vel};
        float[] random_y_vels = new float[5] {- max_random_y_vel, - max_random_y_vel / 2, 0, max_random_y_vel / 2, max_random_y_vel};
        float[] random_z_vels = new float[5] {- max_random_z_vel, - max_random_z_vel / 2, 0, max_random_z_vel / 2, max_random_z_vel};
        if (stopNow == 1){
            System.Random rand = new System.Random();
            int ix = rand.Next(0, 6);
            int iy = rand.Next(0, 6);
            int iz = rand.Next(0, 6);
            random_vel_x = random_x_vels[ix];
            random_vel_y = random_y_vels[iy];
            random_vel_z = random_z_vels[iz];
        } else {
            random_vel_x = 0;
            random_vel_y = 0;
            random_vel_z = 0;
        }
        

        // // もし、止まる時間だったら、止まる。
        // if (stopNow == 1){
        //     random_vel_x = 0;
        //     random_vel_y = 0;
        //     random_vel_z = 0;
        // }
        // // 止まる時間でなく、動いていい範囲内だったらランダムで速度を決める
        // else if(SnitchPosition[0] > xmin && SnitchPosition[0] < xmax && SnitchPosition[1] > ymin && SnitchPosition[1] < ymax && SnitchPosition[2] > zmin && SnitchPosition[2] < zmax){
        //     limitOver = 0;
        //     System.Random rand = new System.Random();
        //     int ix = rand.Next(0, 6);
        //     int iy = rand.Next(0, 6);
        //     int iz = rand.Next(0, 6);
        //     random_vel_x = random_x_vels[ix];
        //     random_vel_y = random_y_vels[iy];
        //     random_vel_z = random_z_vels[iz];
        // } 
        // // 動いていい範囲を超えていたら、元に戻す方向に動くように速度設定をする
        // else {
        //     limitOver = 1;
        //     if(SnitchPosition[0] <= xmin){
        //         random_vel_x = max_x_vel;
        //     }
        //     if(SnitchPosition[0] >= xmax){
        //         random_vel_x = - max_x_vel;
        //     }
        //     if(SnitchPosition[1] <= ymin){
        //         random_vel_y = max_y_vel;
        //     }
        //     if(SnitchPosition[1] >= ymax){
        //         random_vel_y = - max_y_vel;
        //     }
        //     if(SnitchPosition[2] <= zmin) {
        //         random_vel_z = max_z_vel;
        //     }
        //     if(SnitchPosition[2] >= zmax){
        //         random_vel_z = - max_z_vel;
        //     }
        // }
    }

    // VelocityDeterminerで決めた速度で動く時間を決める
    void moveTimeDeterminer(){
        System.Random rand = new System.Random();
        // もし動ける範囲を超えていないのであれば、1 ~ 2秒のランダム時間だけその方向に動く
        if (limitOver == 0){
            int i = rand.Next(0, 6);
            moveDuration = moveDurations[i];
        }
        else{
            moveDuration = 2;
        }
    }

    void stopTimeDeterminer(){
        System.Random rand = new System.Random();
        int i = rand.Next(0, 5);
        stopDuration = stopDurations[i];
    }

    void moveController(){
        transform.position += transform.forward * (vel_x + random_vel_x) * Time.deltaTime;
        transform.position += transform.right * (- vel_z - random_vel_z ) * Time.deltaTime;
        transform.position += transform.up * (vel_y + random_vel_y) * Time.deltaTime;
        seconds += Time.deltaTime;
        if (stopNow == 0){
            if (seconds > moveDuration){
                stopNow = 1;
                seconds = 0;
                stopTimeDeterminer();
            }
        } else {
            if (seconds > stopDuration){
                stopNow = 0;
                seconds = 0;
                moveTimeDeterminer();
                velocityDeterminer();
            }
        }
    }
}
