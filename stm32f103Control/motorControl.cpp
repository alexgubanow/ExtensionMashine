#include "motorControl.h"
#include "endStop.h"
#include "drv8825.h"

extern drv8825 drv;

void motorControl::Run()
{
	drv.EnableDrv();
	drv.Direction((drv8825::movDir)Dir);
	HAL_TIM_PWM_Start(&htim1, TIM_CHANNEL_1);
	TIM1->CCR1 = (speed / 2) - 1;
	TIM1->ARR = speed;
}

void motorControl::Stop()
{
	drv.EnableDrv();
	speed = 0;
	TIM1->CCR1 = (speed / 2) - 1;
	TIM1->ARR = speed;
	HAL_TIM_PWM_Start(&htim1, TIM_CHANNEL_1);
}

void motorControl::Release()
{
	drv.DisableDrv();
	HAL_TIM_PWM_Stop(&htim1, TIM_CHANNEL_1);
	speed = 0;
	TIM1->CCR1 = (speed / 2) - 1;
	TIM1->ARR = speed;
}

void motorControl::Update()
{
	drv.Direction((drv8825::movDir)Dir);
	HAL_TIM_PWM_Stop(&htim1, TIM_CHANNEL_1);
	HAL_TIM_PWM_Start(&htim1, TIM_CHANNEL_1);
	TIM1->CCR1 = (speed / 2) -1;
	TIM1->ARR = speed;
}

motorControl::motorControl()
{
}

motorControl::~motorControl()
{
}
