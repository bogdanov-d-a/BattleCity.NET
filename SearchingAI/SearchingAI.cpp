// AIExample.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "SearchingAI.h"

#define _USE_MATH_DEFINES

#include <math.h>
  
CTankActions tank;

int CompareAngles(int a, int b)///���������� ����
{
    while (a < 0)
    {
		a += 360;
    }
	while (b < 0)
    {
		b += 360;
    }
	int angle = b % 360 - a % 360;
	if(angle > 180)
	{
		angle -= 360;
	}
	if(angle < -180)
	{
		angle += 360;
	}
	return angle;
}

double GetDistance (int x1, int x2, int y1, int y2) // �������� ����������
	{
		return sqrt((float)((x1-x2)*(x1-x2) + (y1-y2)*(y1-y2)));  //���������� �� 1 �� 200
	}

extern "C"
{
	AI_API void SetCoords(int x, int y)
	{
		tank.x = x;
		tank.y = y;
	}
	AI_API void SetAngle(int angle)
	{
		tank.baseAngle = angle;
	}
	AI_API void SetTurretAngle(int angle)
	{
		tank.turretAngle = angle;
	}
	AI_API void SetCollisionStatus(bool isCollided)
	{
	}

	AI_API void SetLivePercent(int percent) 
	{
	}

	AI_API void SetVisilbeEnemyCount(int count) 
	{
		tank.enemies.clear();
		tank.enemies.resize(count);
	}

	AI_API void SetVisibleChests(int count)
	{
	}

	AI_API void SetCoordinatesChest(int id, double x, double y)
	{

	}

	AI_API void SetEnemyProteries(int enemyID, int x, int y, int angle, int turretAngle, int livePercent)
	{
		tank.enemies[enemyID].x = x;
		tank.enemies[enemyID].y = y;
		tank.enemies[enemyID].health = livePercent;
		tank.enemies[enemyID].turretAngle = turretAngle;
	}
	AI_API int GetDirection()
	{
		return 0;
	}
	AI_API int GetRotateDirection()
	{
		if(CompareAngles(tank.baseAngle, tank.movingAngle) > 0)
		{
			return 1;
		}
		if(CompareAngles(tank.baseAngle, tank.movingAngle) < 0)
		{
			return -1;
		}
		if(tank.strateg == searching)
		{
			return 1;
		}
		return 0;
	}
	AI_API int GetRotateSpeed()
	{
		if(tank.strateg == searching)
		{
			return 10;
		}
		if(tank.fireDistance != -1 && abs(CompareAngles(tank.baseAngle, tank.movingAngle)) < 10)
		{
			return abs(CompareAngles(tank.baseAngle, tank.movingAngle));
		}
		return 10;
	}
	AI_API int GetTurretRotateDirection()
	{
		if(CompareAngles(tank.turretAngle, tank.fireAngle) > 0)
		{
			return 1;
		}
		if(CompareAngles(tank.turretAngle, tank.fireAngle) < 0)
		{
			return -1;
		}
		return 0;
	}
	AI_API int GetTurretRotateSpeed()
	{
		if(abs(CompareAngles(tank.turretAngle, tank.fireAngle)) < 20)
		{
			return abs(CompareAngles(tank.turretAngle, tank.fireAngle));
		}
		return 20;
	}
	AI_API int GetFireDistance()
	{
		if(abs(CompareAngles(tank.turretAngle, tank.fireAngle)) < (1800 / tank.fireDistance))
		{
			return tank.fireDistance;
		}
		return -1;
	}

 

	AI_API void Update()
	{
		tank.strateg = searching; //��������� �� ��������� 
		tank.movingAngle = tank.baseAngle;  //���� �������� ��� �� ��� � � ��������� �����
		tank.fireAngle = tank.baseAngle; //���� �������� ��� �� ��� �  � �������
		int preferedEnemy = -1;//���������� �����
		int enemyX = 0;  //���� �
		int enemyY = 0;  // ���� �
		for(unsigned int i = 0; i < tank.enemies.size(); i++)   //������� ���������� ����������� 
		{
			if ((tank.enemies[i].health <= 20))
			{
				preferedEnemy = (int)GetDistance(tank.enemies[i].x, tank.x, tank.enemies[i].y, tank.y) * tank.enemies[i].health;
				enemyX = tank.enemies[i].x;
				enemyY = tank.enemies[i].y;
				break;
			}
			if(GetDistance(tank.enemies[i].x, tank.x, tank.enemies[i].y, tank.y) * tank.enemies[i].health < preferedEnemy || preferedEnemy == -1) // ���� ���� ����� � � ���� ������ ������ ��� ����� �� ���� �� ������� ���� ���������� ������������
			{
				preferedEnemy = (int)GetDistance(tank.enemies[i].x, tank.x, tank.enemies[i].y, tank.y) * tank.enemies[i].health;
				enemyX = tank.enemies[i].x;
				enemyY = tank.enemies[i].y;
			}
		}
		if(preferedEnemy == -1)
		{
			tank.fireDistance = -1;
			return;
		} 
		else
		{
			tank.strateg = camping;
		}
		tank.fireDistance = sqrt((float)((tank.x - enemyX) * (tank.x - enemyX) + (tank.y - enemyY) * (tank.y - enemyY))) / 1; // �������� ��������� ����
		tank.fireAngle = -atan2((float)(enemyY - tank.y), (float)(tank.x - enemyX)) * 180 / M_PI - 90;///��������� ���� ���� ������� �����
		tank.movingAngle = tank.fireAngle;
	}
}

