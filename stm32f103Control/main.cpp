#include "main.h"
#include "comPort.h"
#include "motorControl.h"
#include "endStop.h"
#include "drv8825.h"
#include "hx711.h"

int ibuh = 0;
comPort cP;
motorControl mC;
endStop endStop1;
endStop endStop2;
HX711  hx711sd;
drv8825 drv;

int main(void)
{
	HAL_Init();
	SystemClock_Config();
	initPer();
	for (;;)
	{
		mC.Update();
		endStop1.get();
		endStop2.get();
		if (isCOM_RX)
		{
			cP.parseInStr();
			cP.sendAnswer();
			HAL_GPIO_TogglePin(GPIOC, GPIO_PIN_13);
			isCOM_RX = 0;
		}
	}
}
void initPer()
{
	MX_GPIO_Init();
	MX_TIM1_Init();
	//MX_SPI1_Init();
	MX_USB_DEVICE_Init();
	initEndStops();
	initDrv();
	initHX711();
	mC = motorControl();
}
void initHX711()
{
	HX711::pinsStruct HX711pins;
	HX711pins.CLK = pin(hx711CLK_GPIO_Port, hx711CLK_Pin);
	HX711pins.DATA = pin(hx711DAT_GPIO_Port, hx711DAT_Pin);
	hx711sd = HX711(HX711pins, HX711::hx711Gain::_64);
	hx711sd.set_scale(2000);
	hx711sd.tare(3);
}

void initEndStops()
{
	endStop1 = endStop(endStop1_GPIO_Port, endStop1_Pin);
	endStop2 = endStop(endStop2_GPIO_Port, endStop2_Pin);
}
void initDrv()
{
	drv8825::pinsStruct drvpinsStruct;
	drvpinsStruct.M0 = pin(sp1MOSI_M0_GPIO_Port, sp1MOSI_M0_Pin);
	drvpinsStruct.M1 = pin(sp1CLK_M1_GPIO_Port, sp1CLK_M1_Pin);
	drvpinsStruct.M2 = pin(sp1NSS_M2_GPIO_Port, sp1NSS_M2_Pin);
	drvpinsStruct.EN = pin(motorEN_GPIO_Port, motorEN_Pin);
	drvpinsStruct.SLEEP = pin(motorSLEEP_GPIO_Port, motorSLEEP_Pin);
	drvpinsStruct.RST = pin(sp1MISO_motorReset_GPIO_Port, sp1MISO_motorReset_Pin);
	drvpinsStruct.FAULT = pin(motorFAULT_GPIO_Port, motorFAULT_Pin);
	drvpinsStruct.DIR = pin(motorDIR_GPIO_Port, motorDIR_Pin);
	drv = drv8825(drvpinsStruct, drv8825::microStepRes::step1_8);
}

/** System Clock Configuration
*/
void SystemClock_Config(void)
{
	RCC_OscInitTypeDef RCC_OscInitStruct;
	RCC_ClkInitTypeDef RCC_ClkInitStruct;
	RCC_PeriphCLKInitTypeDef PeriphClkInit;

	/**Initializes the CPU, AHB and APB busses clocks
	*/
	RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
	RCC_OscInitStruct.HSEState = RCC_HSE_ON;
	RCC_OscInitStruct.HSEPredivValue = RCC_HSE_PREDIV_DIV1;
	RCC_OscInitStruct.HSIState = RCC_HSI_ON;
	RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
	RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
	RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL6;
	if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
	{
	}

	/**Initializes the CPU, AHB and APB busses clocks
	*/
	RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK | RCC_CLOCKTYPE_SYSCLK
		| RCC_CLOCKTYPE_PCLK1 | RCC_CLOCKTYPE_PCLK2;
	RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
	RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
	RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
	RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

	if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_1) != HAL_OK)
	{
	}

	PeriphClkInit.PeriphClockSelection = RCC_PERIPHCLK_USB;
	PeriphClkInit.UsbClockSelection = RCC_USBCLKSOURCE_PLL;
	if (HAL_RCCEx_PeriphCLKConfig(&PeriphClkInit) != HAL_OK)
	{
	}

	/**Configure the Systick interrupt time
	*/
	HAL_SYSTICK_Config(HAL_RCC_GetHCLKFreq() / 1000);

	/**Configure the Systick
	*/
	HAL_SYSTICK_CLKSourceConfig(SYSTICK_CLKSOURCE_HCLK);

	/* SysTick_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(SysTick_IRQn, 0, 0);
}