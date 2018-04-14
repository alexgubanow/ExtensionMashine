#include "motorControl.h"
#include "endStop.h"
#include "drv8825.h"

extern drv8825 drv;

void motorControl::Run()
{
	drv.EnableDrv(drv8825::pinState::Enable);
	HAL_TIM_PWM_Start(&htim1, TIM_CHANNEL_1);
	TIM1->CCR1 = speed;
}

void motorControl::Stop()
{
	drv.EnableDrv(drv8825::pinState::Enable);
	HAL_TIM_PWM_Start(&htim1, TIM_CHANNEL_1);
	TIM1->CCR1 = 0;
}

void motorControl::Release()
{
	drv.EnableDrv(drv8825::pinState::Disable);
	HAL_TIM_PWM_Stop(&htim1, TIM_CHANNEL_1);
	speed = 0;
	TIM1->CCR1 = speed;
}

void motorControl::Update()
{
	HAL_TIM_PWM_Stop(&htim1, TIM_CHANNEL_1);
	HAL_TIM_PWM_Start(&htim1, TIM_CHANNEL_1);
	TIM1->CCR1 = speed;
}

motorControl::motorControl()
{
}

motorControl::~motorControl()
{
}
