#pragma once
/* Includes ------------------------------------------------------------------*/
#include "usbd_cdc.h"
extern unsigned char isCOM_RX;
extern USBD_CDC_ItfTypeDef  USBD_Interface_fops_FS;
uint8_t CDC_Transmit_FS(uint8_t* Buf, uint16_t Len);
/* Define size for the receive and transmit buffer over CDC */
/* It's up to user to redefine and/or remove those define */
#define APP_RX_DATA_SIZE  2048
#define APP_TX_DATA_SIZE  2048

/** @defgroup USBD_CDC_Private_Variables
* @{
*/
/* Create buffer for reception and transmission           */
/* It's up to user to redefine and/or remove those define */
/* Received Data over USB are stored in this buffer       */
extern unsigned char UserRxBufferFS[APP_RX_DATA_SIZE];

/* Send Data over USB CDC are stored in this buffer       */
extern unsigned char UserTxBufferFS[APP_TX_DATA_SIZE];