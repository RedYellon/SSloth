#if defined(__arm__)
.text
	.align 3
methods:
	.space 16
	.align 2
Lm_0:
m_WP8PopUps_PopUp_ShowPreLoader_byte:
_m_0:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,203,229,8,208,139,226
	.byte 0,9,189,232,8,112,157,229,0,160,157,232

Lme_0:
	.align 2
Lm_1:
m_WP8PopUps_PopUp_HidePreLoader:
_m_1:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,13,176,160,225,0,208,139,226,0,9,189,232,8,112,157,229
	.byte 0,160,157,232

Lme_1:
	.align 2
Lm_2:
m_WP8PopUps_PopUp_ShowMessageWindow_string_string:
_m_2:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_2:
	.align 2
Lm_3:
m_WP8PopUps_PopUp_ShowMessageWindow_OK_string_string_WP8PopUps_PopUp_Del:
_m_3:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,32,139,229,16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_3:
	.align 2
Lm_4:
m_WP8PopUps_PopUp_ShowMessageWindow_OK_Cancel_string_string_WP8PopUps_PopUp_Del_WP8PopUps_PopUp_Del:
_m_4:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,32,139,229,12,48,139,229,16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_4:
	.align 2
Lm_5:
m_WP8PopUps_PopUp_ShowRateWindow:
_m_5:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,13,176,160,225,0,208,139,226,0,9,189,232,8,112,157,229
	.byte 0,160,157,232

Lme_5:
	.align 2
Lm_6:
m_WP8PopUps_PopUp__ctor:
_m_6:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,8,208,139,226
	.byte 0,9,189,232,8,112,157,229,0,160,157,232

Lme_6:
	.align 2
Lm_c:
m_wrapper_managed_to_native_System_Array_GetGenericValueImpl_int_object_:
_m_c:

	.byte 13,192,160,225,240,95,45,233,120,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229,8,32,139,229
bl p_1

	.byte 16,16,141,226,4,0,129,229,0,32,144,229,0,32,129,229,0,16,128,229,16,208,129,229,15,32,160,225,20,32,129,229
	.byte 0,0,155,229,0,0,80,227,16,0,0,10,0,0,155,229,4,16,155,229,8,32,155,229
bl p_2

	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_WP8PopUps_got - . -4
	.byte 0,0,159,231,0,0,144,229,0,0,80,227,10,0,0,26,16,32,139,226,0,192,146,229,4,224,146,229,0,192,142,229
	.byte 104,208,130,226,240,175,157,232,150,0,160,227,6,12,128,226,2,4,128,226
bl p_3
bl p_4
bl p_5

	.byte 242,255,255,234

Lme_c:
	.align 2
Lm_d:
m_wrapper_delegate_invoke_WP8PopUps_PopUp_Del_invoke_void__this__:
_m_d:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,96,93,45,233,4,208,77,226,13,176,160,225,0,160,160,225,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_WP8PopUps_got - . -4
	.byte 0,0,159,231,0,0,144,229,0,0,80,227,25,0,0,26,44,0,138,226,0,80,144,229,5,0,160,225,0,0,80,227
	.byte 16,0,0,26,16,0,138,226,0,96,144,229,6,0,160,225,0,0,80,227,4,0,0,10,8,0,138,226,0,16,144,229
	.byte 6,0,160,225,49,255,47,225,2,0,0,234,8,0,138,226,0,0,144,229,48,255,47,225,4,208,139,226,96,13,189,232
	.byte 8,112,157,229,0,160,157,232,5,0,160,225,15,224,160,225,12,240,149,229,234,255,255,234
bl p_5

	.byte 227,255,255,234

Lme_d:
	.align 2
Lm_e:
m_wrapper_delegate_begin_invoke_WP8PopUps_PopUp_Del_begin_invoke_IAsyncResult__this___AsyncCallback_object_System_AsyncCallback_object:
_m_e:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,32,139,229,12,0,160,227,7,16,128,226,7,16,193,227,1,208,77,224,0,224,160,227,0,0,0,234,1,224,141,231
	.byte 4,16,81,226,252,255,255,170,0,16,141,226,1,0,160,225,4,32,139,226,0,32,129,229,4,0,128,226,8,32,139,226
	.byte 0,32,128,229,0,0,155,229
bl p_6

	.byte 16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_e:
	.align 2
Lm_f:
m_wrapper_delegate_end_invoke_WP8PopUps_PopUp_Del_end_invoke_void__this___IAsyncResult_System_IAsyncResult:
_m_f:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,0,160,227,7,16,128,226,7,16,193,227,1,208,77,224,0,224,160,227,0,0,0,234,1,224,141,231,4,16,81,226
	.byte 252,255,255,170,0,16,141,226,4,0,139,226,0,0,129,229,0,0,155,229
bl p_7

	.byte 8,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_f:
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
	.long 0
	.align 2
	.long 0
	.align 2
	.long 0
	.align 2
	.long 0
	.align 2
	.long 0
	.align 2
	.long _m_c
	.align 2
	.long _m_d
	.align 2
	.long _m_e
	.align 2
	.long _m_f
.text
	.align 3
method_offsets:

	.long Lm_0 - methods,Lm_1 - methods,Lm_2 - methods,Lm_3 - methods,Lm_4 - methods,Lm_5 - methods,Lm_6 - methods,-1
	.long -1,-1,-1,-1,Lm_c - methods,Lm_d - methods,Lm_e - methods,Lm_f - methods

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

	.byte 0,0
Lm_4_p:

	.byte 0,0
Lm_5_p:

	.byte 0,0
Lm_6_p:

	.byte 0,0
Lm_c_p:

	.byte 0,1,2
Lm_d_p:

	.byte 0,1,2
Lm_e_p:

	.byte 0,0
Lm_f_p:

	.byte 0,0
.text
	.align 3
method_info_offsets:

	.long Lm_0_p - mi,Lm_1_p - mi,Lm_2_p - mi,Lm_3_p - mi,Lm_4_p - mi,Lm_5_p - mi,Lm_6_p - mi,0
	.long 0,0,0,0,Lm_c_p - mi,Lm_d_p - mi,Lm_e_p - mi,Lm_f_p - mi

.text
	.align 3
extra_method_info:

	.byte 0,1,6,83,121,115,116,101,109,46,65,114,114,97,121,58,71,101,116,71,101,110,101,114,105,99,86,97,108,117,101,73
	.byte 109,112,108,32,40,105,110,116,44,111,98,106,101,99,116,38,41,0,1,1,105,110,118,111,107,101,95,118,111,105,100,95
	.byte 95,116,104,105,115,95,95,32,40,41,0,1,2,98,101,103,105,110,95,105,110,118,111,107,101,95,73,65,115,121,110,99
	.byte 82,101,115,117,108,116,95,95,116,104,105,115,95,95,95,65,115,121,110,99,67,97,108,108,98,97,99,107,95,111,98,106
	.byte 101,99,116,32,40,83,121,115,116,101,109,46,65,115,121,110,99,67,97,108,108,98,97,99,107,44,111,98,106,101,99,116
	.byte 41,0,1,3,101,110,100,95,105,110,118,111,107,101,95,118,111,105,100,95,95,116,104,105,115,95,95,95,73,65,115,121
	.byte 110,99,82,101,115,117,108,116,32,40,83,121,115,116,101,109,46,73,65,115,121,110,99,82,101,115,117,108,116,41,0

.text
	.align 3
extra_method_table:

	.long 11,0,0,0,1,12,0,75
	.long 14,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,50,13,11,0
	.long 0,0,162,15,0
.text
	.align 3
extra_method_info_offsets:

	.long 4,12,1,13,50,14,75,15
	.long 162
.text
	.align 3
method_order:

	.long 0,16777215,0,1,2,3,4,5
	.long 6,12,13,14,15

.text
method_order_end:
.text
	.align 3
class_name_table:

	.short 11, 1, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 3
	.short 0, 0, 0, 2, 0, 0, 0
.text
	.align 3
got_info:

	.byte 12,0,39,33,7,17,109,111,110,111,95,103,101,116,95,108,109,102,95,97,100,100,114,0,31,255,254,0,0,0,41,1
	.byte 1,198,0,4,3,0,1,1,2,1,7,30,109,111,110,111,95,99,114,101,97,116,101,95,99,111,114,108,105,98,95,101
	.byte 120,99,101,112,116,105,111,110,95,48,0,7,25,109,111,110,111,95,97,114,99,104,95,116,104,114,111,119,95,101,120,99
	.byte 101,112,116,105,111,110,0,7,35,109,111,110,111,95,116,104,114,101,97,100,95,105,110,116,101,114,114,117,112,116,105,111
	.byte 110,95,99,104,101,99,107,112,111,105,110,116,0,7,26,109,111,110,111,95,100,101,108,101,103,97,116,101,95,98,101,103
	.byte 105,110,95,105,110,118,111,107,101,0,7,24,109,111,110,111,95,100,101,108,101,103,97,116,101,95,101,110,100,95,105,110
	.byte 118,111,107,101,0
.text
	.align 3
got_info_offsets:

	.long 0,2,3
.text
	.align 3
ex_info:
ex:
Le_0_p:

	.byte 44,2,0,0
Le_1_p:

	.byte 36,2,26,0
Le_2_p:

	.byte 48,2,0,0
Le_3_p:

	.byte 52,2,49,0
Le_4_p:

	.byte 56,2,49,0
Le_5_p:

	.byte 36,2,26,0
Le_6_p:

	.byte 44,2,0,0
Le_c_p:

	.byte 128,172,2,75,0
Le_d_p:

	.byte 128,168,2,109,0
Le_e_p:

	.byte 124,2,49,0
Le_f_p:

	.byte 104,2,0,0
.text
	.align 3
ex_info_offsets:

	.long Le_0_p - ex,Le_1_p - ex,Le_2_p - ex,Le_3_p - ex,Le_4_p - ex,Le_5_p - ex,Le_6_p - ex,0
	.long 0,0,0,0,Le_c_p - ex,Le_d_p - ex,Le_e_p - ex,Le_f_p - ex

.text
	.align 3
unwind_info:

	.byte 25,12,13,0,76,14,8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,32,68,13,11,22,12,13,0,76,14
	.byte 8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,13,11,25,12,13,0,76,14,8,135,2,68,14,24,136,6,139
	.byte 5,140,4,142,3,68,14,40,68,13,11,33,12,13,0,72,14,40,132,10,133,9,134,8,135,7,136,6,137,5,138,4
	.byte 139,3,140,2,142,1,68,14,160,1,68,13,11,31,12,13,0,76,14,8,135,2,68,14,36,133,9,134,8,136,7,138
	.byte 6,139,5,140,4,142,3,68,14,40,68,13,11
.text
	.align 3
class_info:
LK_I_0:

	.byte 0,128,144,8,0,0,1
LK_I_1:

	.byte 4,128,152,8,0,0,1,193,0,0,8,193,0,0,5,193,0,0,4,193,0,0,2
LK_I_2:

	.byte 16,128,160,52,0,0,4,193,0,0,8,193,0,3,140,193,0,0,4,193,0,3,139,193,0,3,169,193,0,3,137,193
	.byte 0,3,145,193,0,3,142,193,0,3,141,193,0,3,137,193,0,3,172,193,0,3,169,193,0,3,138,11,10,9
.text
	.align 3
class_info_offsets:

	.long LK_I_0 - class_info,LK_I_1 - class_info,LK_I_2 - class_info


.text
	.align 4
plt:
mono_aot_WP8PopUps_plt:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_WP8PopUps_got - . + 8,0
p_1:
plt__jit_icall_mono_get_lmf_addr:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_WP8PopUps_got - . + 12,4
p_2:
plt__icall_native_System_Array_GetGenericValueImpl_object_int_object_:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_WP8PopUps_got - . + 16,24
p_3:
plt__jit_icall_mono_create_corlib_exception_0:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_WP8PopUps_got - . + 20,42
p_4:
plt__jit_icall_mono_arch_throw_exception:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_WP8PopUps_got - . + 24,75
p_5:
plt__jit_icall_mono_thread_interruption_checkpoint:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_WP8PopUps_got - . + 28,103
p_6:
plt__jit_icall_mono_delegate_begin_invoke:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_WP8PopUps_got - . + 32,141
p_7:
plt__jit_icall_mono_delegate_end_invoke:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_WP8PopUps_got - . + 36,170
plt_end:
.text
	.align 3
mono_image_table:

	.long 2
	.asciz "WP8PopUps"
	.asciz "54832881-B492-4C2D-A4B8-29A198D17E32"
	.asciz ""
	.asciz ""
	.align 3

	.long 0,1,0,0,0
	.asciz "mscorlib"
	.asciz "BDA3258C-0C63-4B93-A52F-C73337202C9E"
	.asciz ""
	.asciz "7cec85d7bea7798e"
	.align 3

	.long 1,2,0,5,0
.data
	.align 3
mono_aot_WP8PopUps_got:
	.space 44
got_end:
.data
	.align 3
mono_aot_got_addr:
	.align 2
	.long mono_aot_WP8PopUps_got
.data
	.align 3
mono_aot_file_info:

	.long 3,44,8,16,1024,1024,128,0
	.long 0,0,0,0,0
.text
	.align 2
mono_assembly_guid:
	.asciz "54832881-B492-4C2D-A4B8-29A198D17E32"
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
	.asciz "WP8PopUps"
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
	.globl _mono_aot_module_WP8PopUps_info
	.align 3
_mono_aot_module_WP8PopUps_info:
	.align 2
	.long Lglobals
.text
	.align 3
mem_end:
#endif
