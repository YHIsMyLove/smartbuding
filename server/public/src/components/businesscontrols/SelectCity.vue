<template>
<div>
    <el-select
        v-model="selecter.value"
        placeholder="请选择城市"
    >
        <el-option
            v-for="item in selecter.datas"
            :key="item.value"
            :label="item.label"
            :value="item.value"
        >
            </el-option>
            </el-select>
</div>
</template>


<script>
import axios from "axios";
import util from "../../common/util";
import NProgress from "nprogress";
export default {
    methods: {
        GetCitys() {
            let that = this;
            axios
                .get("")
                .then(res => {
                    if (!res.data.success) {} else {
                        that.$message({
                            type: "error",
                            message: "获取城市失败!请刷新重试"
                        });
                    }
                })
                .catch(err => console.log(err));
        }
    },
    data() {
        return {
            selecter: {
                datas: [{
                        value: "长沙市",
                        label: "长沙市"
                    },
                    {
                        value: "武汉市",
                        label: "武汉市"
                    }
                ],
                value: ""
            }
        };
    }
};
</script>
