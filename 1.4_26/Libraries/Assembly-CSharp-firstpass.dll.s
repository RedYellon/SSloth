#if defined(__arm__)
.text
	.align 3
methods:
	.space 16
	.align 2
Lm_0:
m_TextureScale__ctor:
_m_0:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,8,208,139,226
	.byte 0,9,189,232,8,112,157,229,0,160,157,232

Lme_0:
	.align 2
Lm_1:
m_TextureScale_Point_UnityEngine_Texture2D_int_int:
_m_1:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,32,139,229,0,0,155,229,4,16,155,229,8,32,155,229,0,48,160,227
bl p_1

	.byte 16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_1:
	.align 2
Lm_2:
m_TextureScale_Bilinear_UnityEngine_Texture2D_int_int:
_m_2:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,32,139,229,0,0,155,229,4,16,155,229,8,32,155,229,1,48,160,227
bl p_1

	.byte 16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_2:
	.align 2
Lm_3:
m_TextureScale_ThreadedScale_UnityEngine_Texture2D_int_int_bool:
_m_3:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,112,93,45,233,80,208,77,226,13,176,160,225,0,80,160,225,20,16,139,229
	.byte 24,32,139,229,28,48,203,229,5,0,160,225,0,224,149,229
bl p_2

	.byte 0,16,160,225,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . -4
	.byte 0,0,159,231,0,16,128,229,20,0,155,229,24,16,155,229,144,1,1,224,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - .
	.byte 0,0,159,231
bl p_3

	.byte 0,16,160,225,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 4
	.byte 0,0,159,231,0,16,128,229,28,0,219,229,0,0,80,227,56,0,0,10,0,42,159,237,0,0,0,234,0,0,128,63
	.byte 194,42,183,238,12,43,139,237,20,0,155,229,16,10,0,238,192,10,184,238,192,42,183,238,14,43,139,237,5,0,160,225
	.byte 0,16,149,229,15,224,160,225,64,240,145,229,12,43,155,237,14,59,155,237,1,0,64,226,16,10,0,238,192,10,184,238
	.byte 192,74,183,238,4,59,131,238,3,43,130,238,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 8
	.byte 0,0,159,231,194,11,183,238,0,10,128,237,0,42,159,237,0,0,0,234,0,0,128,63,194,42,183,238,8,43,139,237
	.byte 24,0,155,229,16,10,0,238,192,10,184,238,192,42,183,238,10,43,139,237,5,0,160,225,0,16,149,229,15,224,160,225
	.byte 56,240,145,229,8,43,155,237,10,59,155,237,1,0,64,226,16,10,0,238,192,10,184,238,192,74,183,238,4,59,131,238
	.byte 3,43,130,238,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 12
	.byte 0,0,159,231,194,11,183,238,0,10,128,237,35,0,0,234,5,0,160,225,0,16,149,229,15,224,160,225,64,240,145,229
	.byte 16,10,0,238,192,10,184,238,192,42,183,238,20,0,155,229,16,10,0,238,192,10,184,238,192,58,183,238,3,43,130,238
	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 8
	.byte 0,0,159,231,194,11,183,238,0,10,128,237,5,0,160,225,0,16,149,229,15,224,160,225,56,240,145,229,16,10,0,238
	.byte 192,10,184,238,192,42,183,238,24,0,155,229,16,10,0,238,192,10,184,238,192,58,183,238,3,43,130,238,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 12
	.byte 0,0,159,231,194,11,183,238,0,10,128,237,5,0,160,225,0,16,149,229,15,224,160,225,64,240,145,229,0,16,160,225
	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 16
	.byte 0,0,159,231,0,16,128,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 20
	.byte 0,0,159,231,20,16,155,229,0,16,128,229
bl p_4

	.byte 24,16,155,229
bl p_5

	.byte 0,64,160,225,24,0,155,229,4,16,160,225
bl p_6

	.byte 0,0,139,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 24
	.byte 0,0,159,231,0,16,160,227,0,16,128,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 28
	.byte 0,0,159,231,0,0,144,229,0,0,80,227,16,0,0,26,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 32
	.byte 0,0,159,231
bl p_7

	.byte 0,32,160,225,2,0,160,225,0,16,160,227,0,224,146,229,64,32,139,229
bl p_8

	.byte 64,16,155,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 28
	.byte 0,0,159,231,0,16,128,229,1,0,84,227,107,0,0,218,0,96,160,227,69,0,0,234,0,0,155,229,150,0,1,224
	.byte 68,16,139,229,1,16,134,226,145,0,0,224,72,0,139,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 36
	.byte 0,0,159,231
bl p_9

	.byte 68,16,155,229,72,32,155,229,64,0,139,229
bl _m_7

	.byte 64,0,155,229,0,160,160,225,28,0,219,229,0,0,80,227,16,0,0,10,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 40
	.byte 0,0,159,231
bl p_10

	.byte 0,16,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 44
	.byte 1,16,159,231,20,16,128,229,0,16,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 48
	.byte 1,16,159,231,12,16,128,229,16,0,139,229,15,0,0,234,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 40
	.byte 0,0,159,231
bl p_10

	.byte 0,16,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 52
	.byte 1,16,159,231,20,16,128,229,0,16,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 48
	.byte 1,16,159,231,12,16,128,229,16,0,139,229,16,0,155,229,4,0,139,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 56
	.byte 0,0,159,231
bl p_7

	.byte 64,0,139,229,16,16,155,229
bl p_11

	.byte 64,0,155,229,8,0,139,229,0,32,160,225,10,16,160,225,0,224,146,229
bl p_12

	.byte 1,96,134,226,1,0,68,226,0,0,86,225,182,255,255,186,0,0,155,229,150,0,0,224,68,0,139,229,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 36
	.byte 0,0,159,231
bl p_9

	.byte 68,16,155,229,64,0,139,229,24,32,155,229
bl _m_7

	.byte 64,0,155,229,0,160,160,225,28,0,219,229,0,0,80,227,2,0,0,10,10,0,160,225
bl p_13

	.byte 4,0,0,234,10,0,160,225
bl p_14

	.byte 1,0,0,234,1,0,160,227
bl p_15

	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 24
	.byte 0,0,159,231,0,0,144,229,4,0,80,225,246,255,255,186,18,0,0,234,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 36
	.byte 0,0,159,231
bl p_9

	.byte 64,0,139,229,0,16,160,227,24,32,155,229
bl _m_7

	.byte 64,0,155,229,12,0,139,229,28,0,219,229,0,0,80,227,2,0,0,10,12,0,155,229
bl p_13

	.byte 1,0,0,234,12,0,155,229
bl p_14

	.byte 5,0,160,225,20,16,155,229,24,32,155,229,0,224,149,229
bl p_16

	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 4
	.byte 0,0,159,231,0,16,144,229,5,0,160,225,0,224,149,229
bl p_17

	.byte 5,0,160,225,0,224,149,229
bl p_18

	.byte 80,208,139,226,112,13,189,232,8,112,157,229,0,160,157,232

Lme_3:
	.align 2
Lm_4:
m_TextureScale_BilinearScale_object:
_m_4:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,112,93,45,233,200,208,77,226,13,176,160,225,184,0,139,229,184,160,155,229
	.byte 10,0,160,225,0,0,80,227,9,0,0,10,0,0,154,229,0,0,144,229,8,0,144,229,4,0,144,229,0,16,159,229
	.byte 0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 60
	.byte 1,16,159,231,1,0,80,225,54,1,0,27,32,160,139,229,8,0,154,229,36,0,139,229,9,1,0,234,36,0,155,229
	.byte 16,10,0,238,192,10,184,238,192,42,183,238,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 12
	.byte 0,0,159,231,0,10,144,237,192,58,183,238,3,43,34,238,194,11,183,238,2,10,13,237,8,0,29,229
bl p_19

	.byte 16,10,2,238,194,42,183,238,194,11,189,238,16,10,16,238,40,0,139,229,0,16,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 16
	.byte 1,16,159,231,0,16,145,229,145,0,6,224,1,0,128,226,0,16,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 16
	.byte 1,16,159,231,0,16,145,229,145,0,0,224,44,0,139,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 20
	.byte 0,0,159,231,0,16,144,229,36,0,155,229,145,0,0,224,48,0,139,229,0,80,160,227,211,0,0,234,16,90,0,238
	.byte 192,10,184,238,192,42,183,238,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 8
	.byte 0,0,159,231,0,10,144,237,192,58,183,238,3,43,34,238,194,11,183,238,2,10,13,237,8,0,29,229
bl p_19

	.byte 16,10,2,238,194,42,183,238,194,11,189,238,16,74,16,238,16,90,0,238,192,10,184,238,192,42,183,238,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 8
	.byte 0,0,159,231,0,10,144,237,192,58,183,238,3,43,34,238,16,74,0,238,192,10,184,238,192,58,183,238,67,43,50,238
	.byte 194,11,183,238,13,10,139,237,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 4
	.byte 0,0,159,231,0,0,144,229,48,16,155,229,5,16,129,224,12,32,144,229,1,0,82,225,214,0,0,155,1,18,160,225
	.byte 1,0,128,224,16,0,128,226,192,0,139,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . -4
	.byte 0,0,159,231,0,0,144,229,4,16,134,224,12,32,144,229,1,0,82,225,201,0,0,155,1,18,160,225,1,0,128,224
	.byte 16,0,128,226,0,16,144,229,72,16,139,229,4,16,144,229,76,16,139,229,8,16,144,229,80,16,139,229,12,0,144,229
	.byte 84,0,139,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . -4
	.byte 0,0,159,231,0,0,144,229,4,16,134,224,1,16,129,226,12,32,144,229,1,0,82,225,180,0,0,155,1,18,160,225
	.byte 1,0,128,224,16,0,128,226,0,16,144,229,88,16,139,229,4,16,144,229,92,16,139,229,8,16,144,229,96,16,139,229
	.byte 12,0,144,229,100,0,139,229,13,10,155,237,192,42,183,238,104,0,139,226,72,16,155,229,76,32,155,229,80,48,155,229
	.byte 84,192,155,229,0,192,141,229,88,192,155,229,4,192,141,229,92,192,155,229,8,192,141,229,96,192,155,229,12,192,141,229
	.byte 100,192,155,229,16,192,141,229,194,11,183,238,5,10,141,237
bl _m_6

	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . -4
	.byte 0,0,159,231,0,0,144,229,44,16,155,229,4,32,129,224,12,48,144,229,2,0,83,225,140,0,0,155,2,34,160,225
	.byte 2,0,128,224,16,0,128,226,0,32,144,229,120,32,139,229,4,32,144,229,124,32,139,229,8,32,144,229,128,32,139,229
	.byte 12,0,144,229,132,0,139,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . -4
	.byte 0,0,159,231,0,0,144,229,4,16,129,224,1,16,129,226,12,32,144,229,1,0,82,225,119,0,0,155,1,18,160,225
	.byte 1,0,128,224,16,0,128,226,0,16,144,229,136,16,139,229,4,16,144,229,140,16,139,229,8,16,144,229,144,16,139,229
	.byte 12,0,144,229,148,0,139,229,13,10,155,237,192,42,183,238,152,0,139,226,120,16,155,229,124,32,155,229,128,48,155,229
	.byte 132,192,155,229,0,192,141,229,136,192,155,229,4,192,141,229,140,192,155,229,8,192,141,229,144,192,155,229,12,192,141,229
	.byte 148,192,155,229,16,192,141,229,194,11,183,238,5,10,141,237
bl _m_6

	.byte 36,0,155,229,16,10,0,238,192,10,184,238,192,42,183,238,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 12
	.byte 0,0,159,231,0,10,144,237,192,58,183,238,3,43,34,238,40,0,155,229,16,10,0,238,192,10,184,238,192,58,183,238
	.byte 67,43,50,238,168,0,139,226,104,16,155,229,108,32,155,229,112,48,155,229,116,192,155,229,0,192,141,229,152,192,155,229
	.byte 4,192,141,229,156,192,155,229,8,192,141,229,160,192,155,229,12,192,141,229,164,192,155,229,16,192,141,229,194,11,183,238
	.byte 5,10,141,237
bl _m_6

	.byte 192,0,155,229,168,16,155,229,0,16,128,229,172,16,155,229,4,16,128,229,176,16,155,229,8,16,128,229,180,16,155,229
	.byte 12,16,128,229,1,80,133,226,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 20
	.byte 0,0,159,231,0,0,144,229,0,0,85,225,36,255,255,186,36,0,155,229,1,0,128,226,36,0,139,229,32,0,155,229
	.byte 12,16,144,229,36,0,155,229,1,0,80,225,240,254,255,186,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 28
	.byte 0,0,159,231,0,16,144,229,1,0,160,225,0,16,145,229,15,224,160,225,80,240,145,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 24
	.byte 0,0,159,231,0,0,144,229,1,16,128,226,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 24
	.byte 0,0,159,231,0,16,128,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 28
	.byte 0,0,159,231,0,16,144,229,1,0,160,225,0,224,145,229
bl p_20

	.byte 200,208,139,226,112,13,189,232,8,112,157,229,0,160,157,232,14,16,160,225,0,0,159,229
bl p_21

	.byte 120,6,0,2,14,16,160,225,0,0,159,229
bl p_21

	.byte 122,6,0,2

Lme_4:
	.align 2
Lm_5:
m_TextureScale_PointScale_object:
_m_5:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,112,89,45,233,20,208,77,226,13,176,160,225,12,0,139,229,12,0,155,229
	.byte 8,0,139,229,12,0,155,229,0,0,80,227,10,0,0,10,8,0,155,229,0,0,144,229,0,0,144,229,8,0,144,229
	.byte 4,0,144,229,0,16,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 60
	.byte 1,16,159,231,1,0,80,225,131,0,0,27,8,0,155,229,0,0,139,229,8,0,155,229,8,0,144,229,4,0,139,229
	.byte 84,0,0,234,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 12
	.byte 0,0,159,231,0,10,144,237,192,42,183,238,4,0,155,229,16,10,0,238,192,10,184,238,192,58,183,238,3,43,34,238
	.byte 194,11,189,238,16,26,16,238,0,32,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 16
	.byte 2,32,159,231,0,32,146,229,146,1,4,224,0,16,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 20
	.byte 1,16,159,231,0,16,145,229,145,0,6,224,0,80,160,227,47,0,0,234,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 4
	.byte 0,0,159,231,0,0,144,229,5,16,134,224,12,32,144,229,1,0,82,225,85,0,0,155,1,18,160,225,1,0,128,224
	.byte 16,0,128,226,0,16,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . -4
	.byte 1,16,159,231,0,16,145,229,16,74,0,238,192,10,184,238,192,42,183,238,0,32,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 8
	.byte 2,32,159,231,0,10,146,237,192,58,183,238,16,90,0,238,192,10,184,238,192,74,183,238,4,59,35,238,3,43,50,238
	.byte 194,11,189,238,16,42,16,238,12,48,145,229,2,0,83,225,58,0,0,155,2,34,160,225,2,16,129,224,16,16,129,226
	.byte 0,32,145,229,0,32,128,229,4,32,145,229,4,32,128,229,8,32,145,229,8,32,128,229,12,16,145,229,12,16,128,229
	.byte 1,80,133,226,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 20
	.byte 0,0,159,231,0,0,144,229,0,0,85,225,200,255,255,186,4,0,155,229,1,0,128,226,4,0,139,229,0,0,155,229
	.byte 12,16,144,229,4,0,155,229,1,0,80,225,165,255,255,186,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 28
	.byte 0,0,159,231,0,16,144,229,1,0,160,225,0,16,145,229,15,224,160,225,80,240,145,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 24
	.byte 0,0,159,231,0,0,144,229,1,16,128,226,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 24
	.byte 0,0,159,231,0,16,128,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 28
	.byte 0,0,159,231,0,16,144,229,1,0,160,225,0,224,145,229
bl p_20

	.byte 20,208,139,226,112,9,189,232,8,112,157,229,0,160,157,232,14,16,160,225,0,0,159,229
bl p_21

	.byte 120,6,0,2,14,16,160,225,0,0,159,229
bl p_21

	.byte 122,6,0,2

Lme_5:
	.align 2
Lm_6:
m_TextureScale_ColorLerpUnclamped_UnityEngine_Color_UnityEngine_Color_single:
_m_6:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,88,208,77,226,13,176,160,225,16,0,139,229,52,16,139,229
	.byte 56,32,139,229,60,48,139,229,112,224,157,229,64,224,139,229,116,224,157,229,68,224,139,229,120,224,157,229,72,224,139,229
	.byte 124,224,157,229,76,224,139,229,128,224,157,229,80,224,139,229,132,224,157,229,84,224,139,229,13,10,155,237,192,42,183,238
	.byte 17,10,155,237,192,58,183,238,13,10,155,237,192,74,183,238,68,59,51,238,21,10,155,237,192,74,183,238,4,59,35,238
	.byte 66,91,176,238,3,91,53,238,14,10,155,237,192,42,183,238,18,10,155,237,192,58,183,238,14,10,155,237,192,74,183,238
	.byte 68,59,51,238,21,10,155,237,192,74,183,238,4,59,35,238,66,75,176,238,3,75,52,238,15,10,155,237,192,42,183,238
	.byte 19,10,155,237,192,58,183,238,15,10,155,237,192,106,183,238,70,59,51,238,21,10,155,237,192,122,183,238,67,107,176,238
	.byte 7,107,38,238,66,59,176,238,6,59,51,238,16,10,155,237,192,42,183,238,20,10,155,237,192,106,183,238,16,10,155,237
	.byte 192,122,183,238,71,107,54,238,21,10,155,237,192,122,183,238,7,107,38,238,6,43,50,238,0,0,160,227,20,0,139,229
	.byte 0,0,160,227,24,0,139,229,0,0,160,227,28,0,139,229,0,0,160,227,32,0,139,229,20,0,139,226,197,11,183,238
	.byte 2,10,13,237,8,16,29,229,196,11,183,238,2,10,13,237,8,32,29,229,195,11,183,238,2,10,13,237,8,48,29,229
	.byte 194,11,183,238,0,10,141,237
bl p_22

	.byte 20,0,155,229,36,0,139,229,24,0,155,229,40,0,139,229,28,0,155,229,44,0,139,229,32,0,155,229,48,0,139,229
	.byte 16,0,155,229,36,16,155,229,0,16,128,229,40,16,155,229,4,16,128,229,44,16,155,229,8,16,128,229,48,16,155,229
	.byte 12,16,128,229,88,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_6:
	.align 2
Lm_7:
m_TextureScale_ThreadData__ctor_int_int:
_m_7:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,32,139,229,4,16,155,229,0,0,155,229,8,16,128,229,8,16,155,229,12,16,128,229,16,208,139,226,0,9,189,232
	.byte 8,112,157,229,0,160,157,232

Lme_7:
	.align 2
Lm_9:
m_wrapper_managed_to_native_System_Array_GetGenericValueImpl_int_object_:
_m_9:

	.byte 13,192,160,225,240,95,45,233,120,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229,8,32,139,229
bl p_23

	.byte 16,16,141,226,4,0,129,229,0,32,144,229,0,32,129,229,0,16,128,229,16,208,129,229,15,32,160,225,20,32,129,229
	.byte 0,0,155,229,0,0,80,227,16,0,0,10,0,0,155,229,4,16,155,229,8,32,155,229
bl p_24

	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 64
	.byte 0,0,159,231,0,0,144,229,0,0,80,227,10,0,0,26,16,32,139,226,0,192,146,229,4,224,146,229,0,192,142,229
	.byte 104,208,130,226,240,175,157,232,150,0,160,227,6,12,128,226,2,4,128,226
bl p_25
bl p_26
bl p_27

	.byte 242,255,255,234

Lme_9:
.text
	.align 3
methods_end:
.data
	.align 3
method_addresses:
	.align 2
	.long _m_0
	.align 2
	.long _m_1
	.align 2
	.long _m_2
	.align 2
	.long _m_3
	.align 2
	.long _m_4
	.align 2
	.long _m_5
	.align 2
	.long _m_6
	.align 2
	.long _m_7
	.align 2
	.long 0
	.align 2
	.long _m_9
.text
	.align 3
method_offsets:

	.long Lm_0 - methods,Lm_1 - methods,Lm_2 - methods,Lm_3 - methods,Lm_4 - methods,Lm_5 - methods,Lm_6 - methods,Lm_7 - methods
	.long -1,Lm_9 - methods

.text
	.align 3
method_info:
mi:
Lm_0_p:

	.byte 0,0
Lm_1_p:

	.byte 0,0
Lm_2_p:

	.byte 0,0
Lm_3_p:

	.byte 0,25,2,3,4,5,6,5,6,7,8,9,10,11,10,12,13,14,15,13,16,15,17,12,9,12,4
Lm_4_p:

	.byte 0,18,18,6,7,7,8,5,5,4,2,2,2,2,6,8,10,9,9,10
Lm_5_p:

	.byte 0,12,18,6,7,8,4,2,5,8,10,9,9,10
Lm_6_p:

	.byte 0,0
Lm_7_p:

	.byte 0,0
Lm_9_p:

	.byte 0,1,19
.text
	.align 3
method_info_offsets:

	.long Lm_0_p - mi,Lm_1_p - mi,Lm_2_p - mi,Lm_3_p - mi,Lm_4_p - mi,Lm_5_p - mi,Lm_6_p - mi,Lm_7_p - mi
	.long 0,Lm_9_p - mi

.text
	.align 3
extra_method_info:

	.byte 0,1,6,83,121,115,116,101,109,46,65,114,114,97,121,58,71,101,116,71,101,110,101,114,105,99,86,97,108,117,101,73
	.byte 109,112,108,32,40,105,110,116,44,111,98,106,101,99,116,38,41,0

.text
	.align 3
extra_method_table:

	.long 11,0,0,0,1,9,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0
.text
	.align 3
extra_method_info_offsets:

	.long 1,9,1
.text
	.align 3
method_order:

	.long 0,16777215,0,1,2,3,4,5
	.long 6,7,9

.text
method_order_end:
.text
	.align 3
class_name_table:

	.short 11, 1, 0, 0, 0, 0, 0, 0
	.short 0, 3, 0, 0, 0, 0, 0, 0
	.short 0, 2, 0, 0, 0, 0, 0
.text
	.align 3
got_info:

	.byte 12,0,39,16,2,0,1,14,194,0,0,0,1,1,129,224,1,16,2,0,2,16,2,0,4,16,2,0,5,16,2,0
	.byte 3,16,2,0,6,16,2,0,7,16,2,0,8,14,134,22,2,14,3,0,14,134,222,2,6,5,30,134,222,2,6,6
	.byte 14,134,30,2,11,3,0,33,3,4,3,193,0,15,249,7,23,109,111,110,111,95,97,114,114,97,121,95,110,101,119,95
	.byte 115,112,101,99,105,102,105,99,0,3,193,0,12,193,3,193,0,22,55,7,14,95,95,101,109,117,108,95,111,112,95,105
	.byte 100,105,118,0,7,24,109,111,110,111,95,111,98,106,101,99,116,95,110,101,119,95,115,112,101,99,105,102,105,99,0,3
	.byte 255,253,0,0,0,9,194,0,56,232,7,23,109,111,110,111,95,111,98,106,101,99,116,95,110,101,119,95,112,116,114,102
	.byte 114,101,101,0,7,20,109,111,110,111,95,111,98,106,101,99,116,95,110,101,119,95,102,97,115,116,0,3,194,0,57,54
	.byte 3,194,0,57,171,3,5,3,6,3,194,0,57,80,3,193,0,16,3,3,193,0,15,241,3,193,0,16,1,3,193,0
	.byte 22,67,3,255,253,0,0,0,9,194,0,56,237,7,32,109,111,110,111,95,97,114,99,104,95,116,104,114,111,119,95,99
	.byte 111,114,108,105,98,95,101,120,99,101,112,116,105,111,110,0,3,193,0,21,10,7,17,109,111,110,111,95,103,101,116,95
	.byte 108,109,102,95,97,100,100,114,0,31,255,254,0,0,0,41,2,2,198,0,4,3,0,1,1,2,2,7,30,109,111,110
	.byte 111,95,99,114,101,97,116,101,95,99,111,114,108,105,98,95,101,120,99,101,112,116,105,111,110,95,48,0,7,25,109,111
	.byte 110,111,95,97,114,99,104,95,116,104,114,111,119,95,101,120,99,101,112,116,105,111,110,0,7,35,109,111,110,111,95,116
	.byte 104,114,101,97,100,95,105,110,116,101,114,114,117,112,116,105,111,110,95,99,104,101,99,107,112,111,105,110,116,0
.text
	.align 3
got_info_offsets:

	.long 0,2,3,7,17,21,25,29
	.long 33,37,41,45,49,52,56,58
	.long 62,64,68,71
.text
	.align 3
ex_info:
ex:
Le_0_p:

	.byte 44,2,0,0
Le_1_p:

	.byte 72,2,26,0
Le_2_p:

	.byte 72,2,26,0
Le_3_p:

	.byte 133,48,2,52,0
Le_4_p:

	.byte 133,64,2,86,0
Le_5_p:

	.byte 130,124,2,121,0
Le_6_p:

	.byte 129,192,2,128,153,0
Le_7_p:

	.byte 72,2,26,0
Le_9_p:

	.byte 128,172,2,128,179,0
.text
	.align 3
ex_info_offsets:

	.long Le_0_p - ex,Le_1_p - ex,Le_2_p - ex,Le_3_p - ex,Le_4_p - ex,Le_5_p - ex,Le_6_p - ex,Le_7_p - ex
	.long 0,Le_9_p - ex

.text
	.align 3
unwind_info:

	.byte 25,12,13,0,76,14,8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,32,68,13,11,25,12,13,0,76,14
	.byte 8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,40,68,13,11,33,12,13,0,76,14,8,135,2,68,14,40
	.byte 132,10,133,9,134,8,136,7,138,6,139,5,140,4,142,3,68,14,120,68,13,11,34,12,13,0,76,14,8,135,2,68
	.byte 14,40,132,10,133,9,134,8,136,7,138,6,139,5,140,4,142,3,68,14,240,1,68,13,11,31,12,13,0,76,14,8
	.byte 135,2,68,14,36,132,9,133,8,134,7,136,6,139,5,140,4,142,3,68,14,56,68,13,11,25,12,13,0,76,14,8
	.byte 135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,112,68,13,11,33,12,13,0,72,14,40,132,10,133,9,134,8
	.byte 135,7,136,6,137,5,138,4,139,3,140,2,142,1,68,14,160,1,68,13,11
.text
	.align 3
class_info:
LK_I_0:

	.byte 0,128,144,8,0,0,1
LK_I_1:

	.byte 4,128,200,8,32,0,1,194,0,0,8,194,0,0,5,194,0,0,4,194,0,0,2
LK_I_2:

	.byte 4,128,128,16,0,0,4,194,0,0,8,194,0,0,5,194,0,0,4,194,0,0,2
.text
	.align 3
class_info_offsets:

	.long LK_I_0 - class_info,LK_I_1 - class_info,LK_I_2 - class_info


.text
	.align 4
plt:
mono_aot_Assembly_CSharp_firstpass_plt:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 76,0
p_1:
plt_TextureScale_ThreadedScale_UnityEngine_Texture2D_int_int_bool:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 80,72
p_2:
plt_UnityEngine_Texture2D_GetPixels:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 84,74
p_3:
plt__jit_icall_mono_array_new_specific:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 88,79
p_4:
plt_UnityEngine_SystemInfo_get_processorCount:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 92,105
p_5:
plt_UnityEngine_Mathf_Min_int_int:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 96,110
p_6:
plt__jit_icall___emul_op_idiv:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 100,115
p_7:
plt__jit_icall_mono_object_new_specific:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 104,132
p_8:
plt_wrapper_remoting_invoke_with_check_System_Threading_Mutex__ctor_bool:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 108,159
p_9:
plt__jit_icall_mono_object_new_ptrfree:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 112,170
p_10:
plt__jit_icall_mono_object_new_fast:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 116,196
p_11:
plt_System_Threading_Thread__ctor_System_Threading_ParameterizedThreadStart:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 120,219
p_12:
plt_System_Threading_Thread_Start_object:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 124,224
p_13:
plt_TextureScale_BilinearScale_object:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 128,229
p_14:
plt_TextureScale_PointScale_object:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 132,231
p_15:
plt_System_Threading_Thread_Sleep_int:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 136,233
p_16:
plt_UnityEngine_Texture2D_Resize_int_int:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 140,238
p_17:
plt_UnityEngine_Texture2D_SetPixels_UnityEngine_Color__:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 144,243
p_18:
plt_UnityEngine_Texture2D_Apply:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 148,248
p_19:
plt_UnityEngine_Mathf_Floor_single:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 152,253
p_20:
plt_wrapper_remoting_invoke_with_check_System_Threading_Mutex_ReleaseMutex:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 156,258
p_21:
plt__jit_icall_mono_arch_throw_corlib_exception:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 160,269
p_22:
plt_UnityEngine_Color__ctor_single_single_single_single:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 164,304
p_23:
plt__jit_icall_mono_get_lmf_addr:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 168,309
p_24:
plt__icall_native_System_Array_GetGenericValueImpl_object_int_object_:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 172,329
p_25:
plt__jit_icall_mono_create_corlib_exception_0:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 176,347
p_26:
plt__jit_icall_mono_arch_throw_exception:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 180,380
p_27:
plt__jit_icall_mono_thread_interruption_checkpoint:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_firstpass_got - . + 184,408
plt_end:
.text
	.align 3
mono_image_table:

	.long 3
	.asciz "Assembly-CSharp-firstpass"
	.asciz "1C9556A6-844B-4313-91D7-7322B2457EF9"
	.asciz ""
	.asciz ""
	.align 3

	.long 0,0,0,0,0
	.asciz "UnityEngine"
	.asciz "3CBF40F4-18AA-4DBF-93A0-11D0C0E1AB57"
	.asciz ""
	.asciz ""
	.align 3

	.long 0,0,0,0,0
	.asciz "mscorlib"
	.asciz "F47783A7-99E9-4AC8-B803-3BD65BBA7EEA"
	.asciz ""
	.asciz "7cec85d7bea7798e"
	.align 3

	.long 1,2,0,5,0
.data
	.align 3
mono_aot_Assembly_CSharp_firstpass_got:
	.space 192
got_end:
.data
	.align 3
mono_aot_got_addr:
	.align 2
	.long mono_aot_Assembly_CSharp_firstpass_got
.data
	.align 3
mono_aot_file_info:

	.long 20,192,28,10,1024,1024,128,0
	.long 0,0,0,0,0
.text
	.align 2
mono_assembly_guid:
	.asciz "1C9556A6-844B-4313-91D7-7322B2457EF9"
.text
	.align 2
mono_aot_version:
	.asciz "66"
.text
	.align 2
mono_aot_opt_flags:
	.asciz "55650815"
.text
	.align 2
mono_aot_full_aot:
	.asciz "TRUE"
.text
	.align 2
mono_runtime_version:
	.asciz ""
.text
	.align 2
mono_aot_assembly_name:
	.asciz "Assembly-CSharp-firstpass"
.text
	.align 3
Lglobals_hash:

	.short 73, 27, 0, 0, 0, 0, 0, 0
	.short 0, 15, 0, 19, 0, 0, 0, 0
	.short 0, 6, 0, 0, 0, 3, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 29
	.short 0, 13, 0, 5, 0, 0, 0, 0
	.short 0, 4, 0, 28, 0, 0, 0, 9
	.short 0, 0, 0, 0, 0, 0, 0, 14
	.short 0, 1, 0, 0, 0, 0, 0, 12
	.short 74, 0, 0, 0, 0, 0, 0, 30
	.short 0, 2, 75, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 22, 0, 0, 0, 0, 0, 0
	.short 0, 11, 0, 17, 0, 8, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 16, 0, 20
	.short 0, 7, 73, 24, 0, 10, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 21, 0, 18, 76, 23, 0, 25
	.short 0, 26, 0
.text
	.align 2
name_0:
	.asciz "methods"
.text
	.align 2
name_1:
	.asciz "methods_end"
.text
	.align 2
name_2:
	.asciz "method_addresses"
.text
	.align 2
name_3:
	.asciz "method_offsets"
.text
	.align 2
name_4:
	.asciz "method_info"
.text
	.align 2
name_5:
	.asciz "method_info_offsets"
.text
	.align 2
name_6:
	.asciz "extra_method_info"
.text
	.align 2
name_7:
	.asciz "extra_method_table"
.text
	.align 2
name_8:
	.asciz "extra_method_info_offsets"
.text
	.align 2
name_9:
	.asciz "method_order"
.text
	.align 2
name_10:
	.asciz "method_order_end"
.text
	.align 2
name_11:
	.asciz "class_name_table"
.text
	.align 2
name_12:
	.asciz "got_info"
.text
	.align 2
name_13:
	.asciz "got_info_offsets"
.text
	.align 2
name_14:
	.asciz "ex_info"
.text
	.align 2
name_15:
	.asciz "ex_info_offsets"
.text
	.align 2
name_16:
	.asciz "unwind_info"
.text
	.align 2
name_17:
	.asciz "class_info"
.text
	.align 2
name_18:
	.asciz "class_info_offsets"
.text
	.align 2
name_19:
	.asciz "plt"
.text
	.align 2
name_20:
	.asciz "plt_end"
.text
	.align 2
name_21:
	.asciz "mono_image_table"
.text
	.align 2
name_22:
	.asciz "mono_aot_got_addr"
.text
	.align 2
name_23:
	.asciz "mono_aot_file_info"
.text
	.align 2
name_24:
	.asciz "mono_assembly_guid"
.text
	.align 2
name_25:
	.asciz "mono_aot_version"
.text
	.align 2
name_26:
	.asciz "mono_aot_opt_flags"
.text
	.align 2
name_27:
	.asciz "mono_aot_full_aot"
.text
	.align 2
name_28:
	.asciz "mono_runtime_version"
.text
	.align 2
name_29:
	.asciz "mono_aot_assembly_name"
.data
	.align 3
Lglobals:
	.align 2
	.long Lglobals_hash
	.align 2
	.long name_0
	.align 2
	.long methods
	.align 2
	.long name_1
	.align 2
	.long methods_end
	.align 2
	.long name_2
	.align 2
	.long method_addresses
	.align 2
	.long name_3
	.align 2
	.long method_offsets
	.align 2
	.long name_4
	.align 2
	.long method_info
	.align 2
	.long name_5
	.align 2
	.long method_info_offsets
	.align 2
	.long name_6
	.align 2
	.long extra_method_info
	.align 2
	.long name_7
	.align 2
	.long extra_method_table
	.align 2
	.long name_8
	.align 2
	.long extra_method_info_offsets
	.align 2
	.long name_9
	.align 2
	.long method_order
	.align 2
	.long name_10
	.align 2
	.long method_order_end
	.align 2
	.long name_11
	.align 2
	.long class_name_table
	.align 2
	.long name_12
	.align 2
	.long got_info
	.align 2
	.long name_13
	.align 2
	.long got_info_offsets
	.align 2
	.long name_14
	.align 2
	.long ex_info
	.align 2
	.long name_15
	.align 2
	.long ex_info_offsets
	.align 2
	.long name_16
	.align 2
	.long unwind_info
	.align 2
	.long name_17
	.align 2
	.long class_info
	.align 2
	.long name_18
	.align 2
	.long class_info_offsets
	.align 2
	.long name_19
	.align 2
	.long plt
	.align 2
	.long name_20
	.align 2
	.long plt_end
	.align 2
	.long name_21
	.align 2
	.long mono_image_table
	.align 2
	.long name_22
	.align 2
	.long mono_aot_got_addr
	.align 2
	.long name_23
	.align 2
	.long mono_aot_file_info
	.align 2
	.long name_24
	.align 2
	.long mono_assembly_guid
	.align 2
	.long name_25
	.align 2
	.long mono_aot_version
	.align 2
	.long name_26
	.align 2
	.long mono_aot_opt_flags
	.align 2
	.long name_27
	.align 2
	.long mono_aot_full_aot
	.align 2
	.long name_28
	.align 2
	.long mono_runtime_version
	.align 2
	.long name_29
	.align 2
	.long mono_aot_assembly_name

	.long 0,0
	.globl _mono_aot_module_Assembly_CSharp_firstpass_info
	.align 3
_mono_aot_module_Assembly_CSharp_firstpass_info:
	.align 2
	.long Lglobals
.text
	.align 3
mem_end:
#endif
