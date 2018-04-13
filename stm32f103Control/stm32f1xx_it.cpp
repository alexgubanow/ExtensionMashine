/* Includes ------------------------------------------------------------------*/
#include "stm32f1xx_hal.h"
#include "stm32f1xx.h"
#include "stm32f1xx_it.h"

/* External variables --------------------------------------------------------*/
extern PCD_HandleTypeDef hpcd_USB_FS;
extern DMA_HandleTypeDef hdma_i2c1_rx;
extern DMA_HandleTypeDef hdma_i2c1_tx;
extern DMA_HandleTypeDef hdma_spi1_rx;
extern DMA_HandleTypeDef hdma_spi1_tx;

/******************************************************************************/
/*            Cortex-M3 Processor Interruption and Exception Handlers         */ 
/******************************************************************************/

/**
* @brief This function handles Non maskable interrupt.
*/
extern "C" void NMI_Handler(void)
{
}

/**
* @brief This function handles Hard fault interrupt.
*/
extern "C" void HardFault_Handler(void)
{
  while (1)
  {
  }
}

/**
* @brief This function handles Memory management fault.
*/
extern "C" void MemManage_Handler(void)
{
  while (1)
  {
  }
}

/**
* @brief This function handles Prefetch fault, memory access fault.
*/
extern "C" void BusFault_Handler(void)
{
  while (1)
  {
  }
}

/**
* @brief This function handles Undefined instruction or illegal state.
*/
extern "C" void UsageFault_Handler(void)
{
  while (1)
  {
  }
}

/**
* @brief This function handles System service call via SWI instruction.
*/
extern "C" void SVC_Handler(void)
{
}

/**
* @brief This function handles Debug monitor.
*/
extern "C" void DebugMon_Handler(void)
{
}

/**
* @brief This function handles Pendable request for system service.
*/
extern "C" void PendSV_Handler(void)
{
}

/**
* @brief This function handles System tick timer.
*/
extern "C" void SysTick_Handler(void)
{
  HAL_IncTick();
  HAL_SYSTICK_IRQHandler();
}

/******************************************************************************/
/* STM32F1xx Peripheral Interrupt Handlers                                    */
/* Add here the Interrupt Handlers for the used peripherals.                  */
/* For the available peripheral interrupt handler names,                      */
/* please refer to the startup file (startup_stm32f1xx.s).                    */
/******************************************************************************/

/**
* @brief This function handles EXTI line0 interrupt.
*/
extern "C" void EXTI0_IRQHandler(void)
{
	/* USER CODE BEGIN EXTI0_IRQn 0 */

	/* USER CODE END EXTI0_IRQn 0 */
	HAL_GPIO_EXTI_IRQHandler(GPIO_PIN_0);
	/* USER CODE BEGIN EXTI0_IRQn 1 */

	/* USER CODE END EXTI0_IRQn 1 */
}

/**
* @brief This function handles USB low priority or CAN RX0 interrupts.
*/
extern "C" void USB_LP_CAN1_RX0_IRQHandler(void)
{
	HAL_PCD_IRQHandler(&hpcd_USB_FS);
}


/**
* @brief This function handles EXTI line[15:10] interrupts.
*/
extern "C" void EXTI15_10_IRQHandler(void)
{
	/* USER CODE BEGIN EXTI15_10_IRQn 0 */

	/* USER CODE END EXTI15_10_IRQn 0 */
	HAL_GPIO_EXTI_IRQHandler(GPIO_PIN_10);
	HAL_GPIO_EXTI_IRQHandler(GPIO_PIN_11);
	/* USER CODE BEGIN EXTI15_10_IRQn 1 */

	/* USER CODE END EXTI15_10_IRQn 1 */
}
