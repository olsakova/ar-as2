﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeviceRotation
{
    private static bool gyroInitialized = false;

    public static bool HasGyroscope
    {
        get
        {
            return SystemInfo.supportsGyroscope;
        }
    }

    public static Quaternion Get()
    {
        if (!gyroInitialized)
        {
            InitGyro();
        }

        return HasGyroscope ? ReadGyroscopeRotation() : Quaternion.identity;
    }

    private static void InitGyro()
    {
        if (HasGyroscope)
        {
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 1f;
        }
        gyroInitialized = true;
    }

    private static Quaternion ReadGyroscopeRotation()
    {
        //return Input.gyro.attitude;
        return new Quaternion(0f, 1f, 1f, 1f) * Input.gyro.attitude * new Quaternion(0, 1, 1, 1);
        //return new Quaternion(0.5f, 0.5f, -0.5f, -0.5f);
    }
}

