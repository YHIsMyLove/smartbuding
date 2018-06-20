<template>
    <f7-page>
        <f7-navbar title="测试传参">
            <f7-nav-left>
                <f7-link icon-f7="arrow_left" @click="backWithParam"></f7-link>
            </f7-nav-left>
        </f7-navbar>
    </f7-page>
</template>
<script>
export default {
    methods: {
        backWithParam() {
            this.$f7.views.main.router.back({
                context: {
                    name: '张三'
                }
            });
        }
    }
}
</script>

