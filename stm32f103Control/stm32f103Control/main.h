#pragma once
#include <stm32f1xx_hal.h>
#include <stm32_hal_legacy.h>
#include "usb_device.h"
#include "usbd_cdc_if.h"
#include "gpio.h"
#include "dma.h"
#include "i2c.h"
#include "spi.h"
#include "tim.h"

void SystemClock_Config(void);