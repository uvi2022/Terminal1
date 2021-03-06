// Decompiled with JetBrains decompiler
// Type: Terminal.NV200.CCommands
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

namespace Terminal.NV200
{
  internal class CCommands
  {
    public const byte SSP_CMD_RESET = 1;
    public const byte SSP_CMD_SET_CHANNEL_INHIBITS = 2;
    public const byte SSP_CMD_DISPLAY_ON = 3;
    public const byte SSP_CMD_DISPLAY_OFF = 4;
    public const byte SSP_CMD_SETUP_REQUEST = 5;
    public const byte SSP_CMD_HOST_PROTOCOL_VERSION = 6;
    public const byte SSP_CMD_POLL = 7;
    public const byte SSP_CMD_REJECT_BANKNOTE = 8;
    public const byte SSP_CMD_DISABLE = 9;
    public const byte SSP_CMD_ENABLE = 10;
    public const byte SSP_CMD_GET_SERIAL_NUMBER = 12;
    public const byte SSP_CMD_UNIT_DATA = 13;
    public const byte SSP_CMD_CHANNEL_VALUE_REQUEST = 14;
    public const byte SSP_CMD_CHANNEL_SECURITY_DATA = 15;
    public const byte SSP_CMD_CHANNEL_RE_TEACH_DATA = 16;
    public const byte SSP_CMD_SYNC = 17;
    public const byte SSP_CMD_LAST_REJECT_CODE = 23;
    public const byte SSP_CMD_HOLD = 24;
    public const byte SSP_CMD_GET_FIRMWARE_VERSION = 32;
    public const byte SSP_CMD_GET_DATASET_VERSION = 33;
    public const byte SSP_CMD_GET_ALL_LEVELS = 34;
    public const byte SSP_CMD_GET_BAR_CODE_READER_CONFIGURATION = 35;
    public const byte SSP_CMD_SET_BAR_CODE_CONFIGURATION = 36;
    public const byte SSP_CMD_GET_BAR_CODE_INHIBIT_STATUS = 37;
    public const byte SSP_CMD_SET_BAR_CODE_INHIBIT_STATUS = 38;
    public const byte SSP_CMD_GET_BAR_CODE_DATA = 39;
    public const byte SSP_CMD_SET_REFILL_MODE = 48;
    public const byte SSP_CMD_PAYOUT_AMOUNT = 51;
    public const byte SSP_CMD_SET_DENOMINATION_LEVEL = 52;
    public const byte SSP_CMD_GET_DENOMINATION_LEVEL = 53;
    public const byte SSP_CMD_COMMUNICATION_PASS_THROUGH = 55;
    public const byte SSP_CMD_HALT_PAYOUT = 56;
    public const byte SSP_CMD_SET_DENOMINATION_ROUTE = 59;
    public const byte SSP_CMD_GET_DENOMINATION_ROUTE = 60;
    public const byte SSP_CMD_FLOAT_AMOUNT = 61;
    public const byte SSP_CMD_GET_MINIMUM_PAYOUT = 62;
    public const byte SSP_CMD_EMPTY_ALL = 63;
    public const byte SSP_CMD_SET_COIN_MECH_INHIBITS = 64;
    public const byte SSP_CMD_GET_NOTE_POSITIONS = 65;
    public const byte SSP_CMD_PAYOUT_NOTE = 66;
    public const byte SSP_CMD_STACK_NOTE = 67;
    public const byte SSP_CMD_FLOAT_BY_DENOMINATION = 68;
    public const byte SSP_CMD_SET_VALUE_REPORTING_TYPE = 69;
    public const byte SSP_CMD_PAYOUT_BY_DENOMINATION = 70;
    public const byte SSP_CMD_SET_COIN_MECH_GLOBAL_INHIBIT = 73;
    public const byte SSP_CMD_SET_GENERATOR = 74;
    public const byte SSP_CMD_SET_MODULUS = 75;
    public const byte SSP_CMD_REQUEST_KEY_EXCHANGE = 76;
    public const byte SSP_CMD_SET_BAUD_RATE = 77;
    public const byte SSP_CMD_GET_BUILD_REVISION = 79;
    public const byte SSP_CMD_SET_HOPPER_OPTIONS = 80;
    public const byte SSP_CMD_GET_HOPPER_OPTIONS = 81;
    public const byte SSP_CMD_SMART_EMPTY = 82;
    public const byte SSP_CMD_CASHBOX_PAYOUT_OPERATION_DATA = 83;
    public const byte SSP_CMD_CONFIGURE_BEZEL = 84;
    public const byte SSP_CMD_POLL_WITH_ACK = 86;
    public const byte SSP_CMD_EVENT_ACK = 87;
    public const byte SSP_CMD_GET_COUNTERS = 88;
    public const byte SSP_CMD_RESET_COUNTERS = 89;
    public const byte SSP_CMD_COIN_MECH_OPTIONS = 90;
    public const byte SSP_CMD_DISABLE_PAYOUT_DEVICE = 91;
    public const byte SSP_CMD_ENABLE_PAYOUT_DEVICE = 92;
    public const byte SSP_CMD_SET_FIXED_ENCRYPTION_KEY = 96;
    public const byte SSP_CMD_RESET_FIXED_ENCRYPTION_KEY = 97;
    public const byte SSP_CMD_REQUEST_TEBS_BARCODE = 101;
    public const byte SSP_CMD_REQUEST_TEBS_LOG = 102;
    public const byte SSP_CMD_TEBS_UNLOCK_ENABLE = 103;
    public const byte SSP_CMD_TEBS_UNLOCK_DISABLE = 104;
    public const byte SSP_POLL_TEBS_CASHBOX_OUT_OF_SERVICE = 144;
    public const byte SSP_POLL_TEBS_CASHBOX_TAMPER = 145;
    public const byte SSP_POLL_TEBS_CASHBOX_IN_SERVICE = 146;
    public const byte SSP_POLL_TEBS_CASHBOX_UNLOCK_ENABLED = 147;
    public const byte SSP_POLL_JAM_RECOVERY = 176;
    public const byte SSP_POLL_ERROR_DURING_PAYOUT = 177;
    public const byte SSP_POLL_SMART_EMPTYING = 179;
    public const byte SSP_POLL_SMART_EMPTIED = 180;
    public const byte SSP_POLL_CHANNEL_DISABLE = 181;
    public const byte SSP_POLL_INITIALISING = 182;
    public const byte SSP_POLL_COIN_MECH_ERROR = 183;
    public const byte SSP_POLL_EMPTYING = 194;
    public const byte SSP_POLL_EMPTIED = 195;
    public const byte SSP_POLL_COIN_MECH_JAMMED = 196;
    public const byte SSP_POLL_COIN_MECH_RETURN_PRESSED = 197;
    public const byte SSP_POLL_PAYOUT_OUT_OF_SERVICE = 198;
    public const byte SSP_POLL_NOTE_FLOAT_REMOVED = 199;
    public const byte SSP_POLL_NOTE_FLOAT_ATTACHED = 200;
    public const byte SSP_POLL_NOTE_TRANSFERED_TO_STACKER = 201;
    public const byte SSP_POLL_NOTE_PAID_INTO_STACKER_AT_POWER_UP = 202;
    public const byte SSP_POLL_NOTE_PAID_INTO_STORE_AT_POWER_UP = 203;
    public const byte SSP_POLL_NOTE_STACKING = 204;
    public const byte SSP_POLL_NOTE_DISPENSED_AT_POWER_UP = 205;
    public const byte SSP_POLL_NOTE_HELD_IN_BEZEL = 206;
    public const byte SSP_POLL_BAR_CODE_TICKET_ACKNOWLEDGE = 209;
    public const byte SSP_POLL_DISPENSED = 210;
    public const byte SSP_POLL_JAMMED = 213;
    public const byte SSP_POLL_HALTED = 214;
    public const byte SSP_POLL_FLOATING = 215;
    public const byte SSP_POLL_FLOATED = 216;
    public const byte SSP_POLL_TIME_OUT = 217;
    public const byte SSP_POLL_DISPENSING = 218;
    public const byte SSP_POLL_NOTE_STORED_IN_PAYOUT = 219;
    public const byte SSP_POLL_INCOMPLETE_PAYOUT = 220;
    public const byte SSP_POLL_INCOMPLETE_FLOAT = 221;
    public const byte SSP_POLL_CASHBOX_PAID = 222;
    public const byte SSP_POLL_COIN_CREDIT = 223;
    public const byte SSP_POLL_NOTE_PATH_OPEN = 224;
    public const byte SSP_POLL_NOTE_CLEARED_FROM_FRONT = 225;
    public const byte SSP_POLL_NOTE_CLEARED_TO_CASHBOX = 226;
    public const byte SSP_POLL_CASHBOX_REMOVED = 227;
    public const byte SSP_POLL_CASHBOX_REPLACED = 228;
    public const byte SSP_POLL_BAR_CODE_TICKET_VALIDATED = 229;
    public const byte SSP_POLL_FRAUD_ATTEMPT = 230;
    public const byte SSP_POLL_STACKER_FULL = 231;
    public const byte SSP_POLL_DISABLED = 232;
    public const byte SSP_POLL_UNSAFE_NOTE_JAM = 233;
    public const byte SSP_POLL_SAFE_NOTE_JAM = 234;
    public const byte SSP_POLL_NOTE_STACKED = 235;
    public const byte SSP_POLL_NOTE_REJECTED = 236;
    public const byte SSP_POLL_NOTE_REJECTING = 237;
    public const byte SSP_POLL_CREDIT_NOTE = 238;
    public const byte SSP_POLL_READ_NOTE = 239;
    public const byte SSP_POLL_SLAVE_RESET = 241;
    public const byte SSP_RESPONSE_OK = 240;
    public const byte SSP_RESPONSE_COMMAND_NOT_KNOWN = 242;
    public const byte SSP_RESPONSE_WRONG_NO_PARAMETERS = 243;
    public const byte SSP_RESPONSE_PARAMETER_OUT_OF_RANGE = 244;
    public const byte SSP_RESPONSE_COMMAND_CANNOT_BE_PROCESSED = 245;
    public const byte SSP_RESPONSE_SOFTWARE_ERROR = 246;
    public const byte SSP_RESPONSE_FAIL = 248;
    public const byte SSP_RESPONSE_KEY_NOT_SET = 250;
  }
}
