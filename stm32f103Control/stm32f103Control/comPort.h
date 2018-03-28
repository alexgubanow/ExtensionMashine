#pragma once
#include <stm32f1xx_hal.h>
#include <stm32_hal_legacy.h>
#include "usbd_cdc_if.h"
#include "usb_device.h"
#include "jsmn.h"
#include "JSONpacker.h"

//extern dlis2k dlis2kinst;
//extern icTW28 icTW28inst;
//extern icMSB icMSBinst;
//extern icMCB icMCBinst;
//extern imageProc imageProcinst;
//extern posCalc posCalcinst;

class comPort
{
	/**
	* @brief  waitForTXtypes types
	*/
	typedef enum waitForTXtypes {
		NoWait,
		Wait
	}waitForTXtype;
	const char ResolutionTypesStrings[10][6] = { "um0_1", "um0_2", "um0_3", "um0_4", "um0_5", "um0_6", "um0_7", "um0_8", "um0_9", "um1_0" };
public:
	int resultCode;
	jsmn_parser p;
	jsmntok_t tokens[16];

	char commRx[128];
	char data1[128];
	char data2[128];

	comPort();
	~comPort();

	void parseInStr();
	void sendAnswer();
	USBD_StatusTypeDef sendStr(const char * commTx, const char * key, char * str, waitForTXtype waitForTX);
	void waitUntilTXend(USBD_HandleTypeDef *pdev);
};
