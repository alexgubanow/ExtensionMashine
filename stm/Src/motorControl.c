/**
* vel=vel0+acc*t; vel0=0 --> vel=acc*t;
*
* Sys clk period = 1sec/(system clock speed)
* TIM period = (prescaler)*1sec/(system clock speed)
*
* (TIM1) consist of a 16-bit auto-reload counter driven by a programmable prescaler.
*   TIM_InitStruct.CounterMode = LL_TIM_COUNTERMODE_UP;
* In upcounting mode, the counter counts from 0 to the auto-reload value (content of the
TIMx_ARR register), then restarts from 0 and generates a counter overflow event.

* TIM1 length t (sec) = (Autoreload+1)*(Prescaler)/(SystemCoreClock) = 1.3653125 sec
* 1 sec = tim1AccEnd + 1 [ticks] = 1*(SystemCoreClock)/(Prescaler) = 732.4330510 ticks = approx 733 ticks
**
*   SystemCoreClock LL_SetSystemCoreClock(48000000);
*   adc1_tim1Start
*   TIM1: TIM_InitStruct.Prescaler = 65535;   TIM_InitStruct.Autoreload = 999;
*   TIM14: TIM_InitStruct.Prescaler = 1;   TIM_InitStruct.Autoreload = 0; LL_TIM_SetAutoReload(TIM14, newVel);
*
* 	__HAL_TIM_GET_CLOCKDIVISION(TIM1);
*
* void setMotorVel(unsigned int vel)
*
*/

#pragma once

#include "motorControl.h"
#include "system_stm32f0xx.h"
#include "tim.h"

void runMotorAccel(unsigned int vel)
{
	unsigned int oneSec = SystemCoreClock / __HAL_TIM_GET_CLOCKDIVISION(TIM1); //+1 sec [733ticks]

	uint16_t tim1AccStart = __HAL_TIM_GET_COUNTER(TIM1);
	//uint16_t tim1AccEnd = tim1AccStart + oneSec;
	//if (tim1AccEnd > __HAL_TIM_GET_AUTORELOAD(TIM1))
	//{
	//	tim1AccEnd -= __HAL_TIM_GET_AUTORELOAD(TIM1);
	//}

	unsigned int i = 0;

	while (i <= oneSec || !startTrigger || !endTrigger)	//how to make it non interupting?
	{
		if (tim1AccStart != __HAL_TIM_GET_COUNTER(TIM1)) //asuming it gets called at least once every TIM1 tick
		{
			unsigned int velAcc = vel * i / oneSec;
			setMotorVel(velAcc);
			tim1AccStart = __HAL_TIM_GET_COUNTER(TIM1);
			i++;
		}
	}
}