#!/usr/bin/env python
# coding=utf-8

import xlrd
import os
import sys
import google
import re

from google.protobuf.descriptor import FieldDescriptor

sys.path.append("./")

import table_common_pb2

curr_row = 0
FIELD_INDEX_NAME = 0
FIELD_INDEX_NUMBER = 1
FIELD_INDEX_TYPE = 2
FIELD_INDEX_LABEL = 3


def ParseItemDrop(row_value):
    pattern = '^([0-9]*,[0-9]*(,[0-9]*){0,2})(\^[0-9]*,[0-9]*(,[0-9]*){0,2})*$'
    if not re.match(pattern, row_value):
        print("invalid drop format: " + row_value)
        return None

    drop_list = row_value.split('^')
    item_drop = table_common_pb2.ItemDrop()
    for drop in drop_list:
        print("2")
        triple = drop.split(",")
        added_drop = item_drop.drop_list.add()
        added_drop.id = int(triple[0])
        added_drop.count = int(triple[1]) if len(triple[1]) else 1
        print("2")

        if len(triple) > 2 and len(triple[2]):
            added_drop.prob = int(triple[2])
        else:
            item_drop.prob_type = item_drop.PROB_TYPE_EQUAL

        print("2")
        if len(triple) > 3 and len(triple[3]):
            added_drop.max_times = int(triple[3])

    return item_drop


def ParseZuoqiPro(row_value):
    pattern = '^([0-9]{1,3})(\^([0-9]{1,3}))*$'
    if not re.match(pattern, row_value):
        print("invalid drop format: " + row_value)
        return None

    item_drop = table_common_pb2.ZuoqiPro()
    drop_list = row_value.split('^')
    for drop in drop_list:
        triple = drop.split(",")
        added_drop = item_drop.zuoqieffect.add()
        added_drop.type = int(triple[0])
    # added_drop.initial_value = int(triple[1])
    # added_drop.add_value = int(triple[2])

    return item_drop


def ParseActivityAwardList(row_value):
    pattern = '^([0-9]*,[0-9]*(,[0-9]*){0,2})(\^[0-9]*,[0-9]*(,[0-9]*){0,2})*$'
    if not re.match(pattern, row_value):
        print("invalid drop format: " + row_value)
        return None

    drop_list = row_value.split('^')
    item_drop = table_common_pb2.ActivityAwardList()
    for drop in drop_list:
        triple = drop.split(",")
        added_drop = item_drop.award.add()
        added_drop.item_id = int(triple[0])
        added_drop.item_count = int(triple[1])
        added_drop.lev = int(triple[2])

    return item_drop


##########################

##########################
def ParseRange(row_value):
    pattern = '^[\[\(]{1}[0-9]+,[0-9]+[\]\)]'
    if not re.match(pattern, row_value):
        print(row_value)
        print("invalid range format: " + row_value)
        return None

    range_value = table_common_pb2.Range()
    range_value.range_type = range_value.RANGE_TYPE_INVALID
    if row_value[0] == '[' and row_value[-1] == ']':
        range_value.range_type = range_value.RANGE_TYPE_CLOSED_CLOSED
    elif row_value[0] == '[' and row_value[-1] == ')':
        range_value.range_type = range_value.RANGE_TYPE_CLOSED_OPEN
    elif row_value[0] == '(' and row_value[-1] == ']':
        range_value.range_type = range_value.RANGE_TYPE_OPEN_CLOSED
    elif row_value[0] == '(' and row_value[-1] == ')':
        range_value.range_type = range_value.RANGE_TYPE_OPEN_OPEN
    else:
        print("what happens: " + row_value)
        exit(1)

    pair = row_value[1:-1].split(',')
    range_value.lhs = int(pair[0])
    range_value.rhs = int(pair[1])
    return range_value


##########################
def ParseIntPair(row_value):
    print("int_pair = " + row_value)
    print("int_pair = " + row_value)
    pair = table_common_pb2.IntPair()
    list = row_value.split('/')
    lenght = int(len(list))
    if lenght >= 1:
        pair.molecule = int(list[0])
    if lenght >= 2:
        pair.denominator = int(list[1])
    return pair


def ParseIntPairList(row_value):
    pattern = '/^([0-9]*,[0-9]*^\^)*([0-9]*,[0-9]*)$'
    if not re.match(pattern, row_value):
        print("invalid IntPairList format: " + row_value)
        return None

    int_pair_list = table_common_pb2.IntPairList()

    pair_list = row_value.split('^')
    for data in pair_list:
        strs = data.split('/')
        int_pari = int_pair_list.list.add()
        pair = ParseIntPair(strs)
        int_pari.append(pair)

    return int_pair_list


def ParseIntTupleList(row_value):
    pattern = '^([0-9]*,[0-9]*,[0-9]*\^)*([0-9]*,[0-9]*,[0-9]*)$'
    if not re.match(pattern, row_value):
        print("invalid IntTupleList format: " + row_value)
        return None

    int_tuple_list = table_common_pb2.IntTupleList()

    tuple_list = row_value.split('^')
    for _tuple in tuple_list:
        __tuple = _tuple.split(',')
        int_tuple = int_tuple_list.list.add()
        int_tuple.first = int(__tuple[0])
        int_tuple.second = int(__tuple[1])
        int_tuple.third = int(__tuple[2])

    return int_tuple_list


def ParseIntList(row_value):
    int_list = table_common_pb2.IntList()
    list = row_value.split('/')
    for v in list:
        int_list.list.append(int(v))
    return int_list


##### New Struct #####
def ParseIntListList(row_value):
    '''
    pattern = '^([0-9]*,[0-9]*,[0-9]*\^)*([0-9]*,[0-9]*,[0-9]*)$'
    if not re.match(pattern, row_value):
        ERROR("invalid IntListList format: " + row_value)
        return None
    '''

    int_list_list: table_common_pb2.IntListList = table_common_pb2.IntListList()
    int_list: table_common_pb2.IntList = int_list_list.list

    list_group_one = str(row_value).split('/')
    for int_group in list_group_one:
        int_s = int_group.split("#")
        internal_int_list = table_common_pb2.IntList()
        for v in int_s:
            internal_int_list.list.append(int(v))
        int_list.append(internal_int_list)
    return int_list_list
    
def ParseIntListJingHao(row_value):
    int_list = table_common_pb2.IntListJingHao()
    mList = str(row_value).split('#')
    for v in mList:
        int_list.list.append(int(v))
    return int_list
    
def ParseInt32ListJingHao(row_value):
    int_list = table_common_pb2.Int32ListJingHao()
    mList = str(row_value).split('#')
    for v in mList:
        int_list.list.append(int(v))
    return int_list


def ParseIntListXiaHuaXian(row_value):
    int_list = table_common_pb2.IntListXiaHuaXian()
    list = str(row_value).split('_')
    for v in list:
        int_list.list.append(int(v))
    return int_list


def ParseIntListJingHaoMeiYuan(row_value):
    int_list_list = table_common_pb2.IntListJingHaoMeiYuan()
    list_list = str(row_value).split('&')
    for f_list in list_list:
        internal_list = int_list_list.list.add()
        int_list = f_list.split('#')
        for v in int_list:
            internal_list.list.append(int(v))
    return int_list_list


def ParseFenHao(row_value):
    int_list_list = table_common_pb2.IntListXiaHuaXianFenHao()
    if row_value.strip() == '':
        return int_list_list
    list_list = row_value.split(';')
    for f_list in list_list:
        internal_list = int_list_list.list.add()
        int_list = f_list.split('_')
        for v in int_list:
            internal_list.list.append(int(v))
    return int_list_list


def ParseStringList(row_value):
    int_list = table_common_pb2.StringList()
    list = row_value.split('#')
    for v in list:
        int_list.list.append(v)
    return int_list


def ParseIntSpecXiaHuaJinHao(row_value):
    int_list = table_common_pb2.IntSpecXiaHuaJinHao()
    list = row_value.split('#')
    index = 0
    for v in list:
        if index == 0:
            list1 = v.split('_')
            int_list.value0 = int(list1[0])
            int_list.list.append(int(list1[1]))
        else:
            int_list.list.append(int(v))
        index = index + 1
    return int_list


##### main process #####
def Process(excel_Name, table_head):
    if table_head == None:
        excel_file = './../workbook/' + excel_Name + '.xlsx'
    else:
        excel_file = './../workbook/' + table_head + excel_Name + '.xlsx'
    entry_name = str(excel_Name).upper()
    entry_list_name = entry_name + 'ARRAY'

    tbl_file = str(excel_Name).lower() + ".bytes"
    pb2 = excel_Name + "_pb2"
    module = __import__(pb2)

    proto_desc = []

    # filedDescriptor = google.protobuf.descriptor.FieldDescriptor
    attr = getattr(module, '_' + entry_name).fields
    for desc in getattr(module, '_' + entry_name).fields:
        if desc.number > 99:
            continue
        field_name = desc.name

        if field_name[0] == '_':
            continue
        field_number = desc.number
        field_label = desc.label
        field_type = None
        if desc.type == desc.TYPE_INT32 or \
                desc.type == desc.TYPE_INT64 or \
                desc.type == desc.TYPE_SINT32 or \
                desc.type == desc.TYPE_SINT64 or \
                desc.type == desc.TYPE_FIXED32 or \
                desc.type == desc.TYPE_FIXED64 or \
                desc.type == desc.TYPE_UINT32 or \
                desc.type == desc.TYPE_UINT64 or \
                desc.type == desc.TYPE_ENUM:
            field_type = int
        elif desc.type == desc.TYPE_BOOL:
            field_type = bool
        elif desc.type == desc.TYPE_FLOAT:
            field_type = float
        elif desc.type == desc.TYPE_BYTES or \
                desc.type == desc.TYPE_STRING:
            field_type = str
        elif desc.type == desc.TYPE_MESSAGE:
            msg_desc = desc.message_type
            if msg_desc.name == "ItemDrop":
                field_type = table_common_pb2.ItemDrop
            elif msg_desc.name == "Range":
                field_type = table_common_pb2.Range
            elif msg_desc.name == "IntPairList":
                field_type = table_common_pb2.IntPairList
            elif msg_desc.name == "ZuoqiPro":
                field_type = table_common_pb2.ZuoqiPro
            elif msg_desc.name == "ActivityAwardList":
                field_type = table_common_pb2.ActivityAwardList
            elif msg_desc.name == "IntListList":
                field_type = table_common_pb2.IntListList
            elif msg_desc.name == "IntTupleList":
                field_type = table_common_pb2.IntTupleList
            elif msg_desc.name == "IntList":
                field_type = table_common_pb2.IntList
            elif msg_desc.name == "IntPair":
                field_type = table_common_pb2.IntPair
            elif msg_desc.name == "IntListJingHao":
                field_type = table_common_pb2.IntListJingHao
            elif msg_desc.name == "Int32ListJingHao":
                field_type = table_common_pb2.Int32ListJingHao
            elif msg_desc.name == "IntListXiaHuaXian":
                field_type = table_common_pb2.IntListXiaHuaXian
            elif msg_desc.name == "IntListJingHaoMeiYuan":
                field_type = table_common_pb2.IntListJingHaoMeiYuan
            elif msg_desc.name == "IntListXiaHuaXianFenHao":
                field_type = table_common_pb2.IntListXiaHuaXianFenHao
            elif msg_desc.name == "StringList":
                field_type = table_common_pb2.StringList
            elif msg_desc.name == "IntSpecXiaHuaJinHao":
                field_type = table_common_pb2.IntSpecXiaHuaJinHao
        else:
            print('不支持的类型')
        proto_desc += [(field_name, field_number, field_type, field_label)]
    book = xlrd.open_workbook(excel_file)
    sheet = book.sheet_by_index(0)
    row_array = getattr(module, entry_list_name)()

    global curr_row
    for curr_row in range(sheet.nrows):
        if curr_row <= 3:  # 第一行是标题首充禮包
            continue
        row_values = sheet.row_values(curr_row)

        row = row_array.rows.add()
        for field_desc in proto_desc:
            row_value = row_values[field_desc[FIELD_INDEX_NUMBER] - 1]
            if row_value != "" and type(row_value)==float:
                row_value = str(int(row_value));

            if field_desc[FIELD_INDEX_TYPE] == str:
                setattr(row, field_desc[FIELD_INDEX_NAME], str(row_value))
            elif field_desc[FIELD_INDEX_TYPE] == int:
                if field_desc[FIELD_INDEX_LABEL] == FieldDescriptor.LABEL_REPEATED:
                    v = str(row_value).strip(). \
                        replace('|', '^') \
                        .replace('/', '^') \
                        .replace('#', '^') \
                        .replace('&', '^') \
                        .split('^')
                    for section in v:
                        if section.strip():
                            getattr(row, field_desc[FIELD_INDEX_NAME]).append(int(section.strip()))
                else:
                    if row_value == '':
                        row_value = '0'
                    # row_value = int(0) if row_value == '' else int(row_value)
                    setattr(row, field_desc[FIELD_INDEX_NAME], int(row_value))
                # if row_value == '':
                #     row_value = int(0)

                # # row_value = int(0) if row_value == '' else int(row_value)
                # setattr(row, field_desc[FIELD_INDEX_NAME], int(row_value))
                

            elif field_desc[FIELD_INDEX_TYPE] == bool:
                if row_value == '':
                    row_value = 0
                # row_value = int(0) if row_value == '' else int(row_value)
                setattr(row, field_desc[FIELD_INDEX_NAME], bool(row_value))
            elif field_desc[FIELD_INDEX_TYPE] == float:
                if row_value == '':
                    row_value = float(0)
                    # else float(row_value)
                setattr(row, field_desc[FIELD_INDEX_NAME], float(row_value))
                # µôÂä
            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.ItemDrop:
                if row_value != "":
                    item_drop = ParseItemDrop(row_value)
                    if not item_drop:
                        print("pass item drop failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(item_drop)

            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.ActivityAwardList:
                if row_value != "":
                    item_drop = ParseActivityAwardList(row_value)
                    if not item_drop:
                        print("pass item drop failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(item_drop)

            # Çø¼ä
            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.Range:
                if row_value != "":
                    range_value = ParseRange(row_value)
                    if not range_value:
                        print("pass range failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(range_value)
            # ÁÐ±í
            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntList:
                if row_value != "":
                    int_list = ParseIntList(row_value)
                    if not int_list:
                        print("pass IntList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_list)

            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntPair:
                if row_value != "":
                    int_pair = ParseIntPair(row_value)
                    if not int_pair:
                        print("pass IntPair failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_pair)

            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntPairList:
                if row_value != "":
                    int_pair_list = ParseIntPairList(row_value)
                    if not int_pair_list:
                        print("pass IntPairList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_pair_list)

            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntTupleList:
                if row_value != "":
                    int_tuple_list = ParseIntTupleList(row_value)
                    if not int_tuple_list:
                        print("pass IntTupleList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_tuple_list)

            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntListList:
                if row_value != "":
                    int_list_list = ParseIntListList(row_value)
                    if not int_list_list:
                        print("pass IntListList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_list_list)

            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntListJingHao:
                if row_value != "":
                    int_list = ParseIntListJingHao(row_value)
                    if not int_list:
                        print("pass IntList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_list)

            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.Int32ListJingHao:
                if row_value != "":
                    int_list = ParseInt32ListJingHao(row_value)
                    if not int_list:
                        print("pass IntList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_list)
            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntListXiaHuaXian:
                if row_value != "":
                    int_list = ParseIntListXiaHuaXian(row_value)
                    if not int_list:
                        print("pass IntList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_list)
            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntListJingHaoMeiYuan:
                if row_value != "":
                    int_list = ParseIntListJingHaoMeiYuan(row_value)

                    if not int_list:
                        print("pass IntList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_list)
            # elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntListXiaHuaXianFenHao:
            #
            #     if row_value != "":
            #
            #         int_list = ParseIntListXiaHuaXianFenHao(row_value)
            #
            #         if not int_list:
            #             print("pass IntList failed: " + row_value);
            #             exit(1)
            #         getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_list)
            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.StringList:

                if row_value != "":

                    int_list = ParseStringList(row_value)

                    if not int_list:
                        print("pass IntList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_list)
            elif field_desc[FIELD_INDEX_TYPE] == table_common_pb2.IntSpecXiaHuaJinHao:
                if row_value != "":

                    int_list = ParseIntSpecXiaHuaJinHao(row_value)

                    if not int_list:
                        print("pass IntList failed: " + row_value)
                        exit(1)
                    getattr(row, field_desc[FIELD_INDEX_NAME]).CopyFrom(int_list)
            else:
                print("错误数据："+str(curr_row))
    f = open(tbl_file, 'wb')
    f.write(row_array.SerializeToString())
    f.close()


if __name__ == "__main__":
    table_name = None
    table_head = None
    for arg in sys.argv[1:]:
        if table_name is None:
            table_name = arg
        elif table_head is None:
            table_head = arg
    # try:
    Process(table_name, table_head)
    # except BaseException as e:
    # print("打包错误")
    # exit(1)
    exit(0)


