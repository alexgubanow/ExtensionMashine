#pragma once
#include <stm32f1xx_hal.h>
#include <stm32_hal_legacy.h>
#include "usbd_cdc_if.h"
#include "usb_device.h"
#include "jsmn.h"
#include "JSONpacker.h"
#include "motorControl.h"

class comPort
{
	/**
	* @brief  waitForTXtypes types
	*/
	typedef enum waitForTXtypes {
		NoWait,
		Wait
	}waitForTXtype;
public:
	int resultCode;
	jsmn_parser p;
	jsmntok_t tokens[16];

	char commRx[128];

	comPort();
	~comPort();

	void parseInStr();
	void sendAnswer();
	USBD_StatusTypeDef sendStr(const char * commTx, const char * key, char * str, waitForTXtype waitForTX);
	void waitUntilTXend(USBD_HandleTypeDef *pdev);
};
