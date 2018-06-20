<template>
    <f7-page>
        <f7-navbar title="通讯录" back-link="Back"></f7-navbar>
        <f7-list contacts-list>
            <f7-list-group :key="key1" v-for="(group, key1) in contacts">
                <f7-list-item :title="key1" group-title></f7-list-item>
                <f7-list-item :key="key2" v-for="(name, key2) in group" :title="name" link="/workers/detail/"></f7-list-item>
            </f7-list-group>
        </f7-list>
    </f7-page>
</template>
<script>
export default {
  data() {
    return {
      contacts: {
        A: [
          "Aaron",
          "Abbie",
          "Adam",
          "Adele",
          "Agatha",
          "Agnes",
          "Albert",
          "Alexander"
        ],
        B: ["Bailey", "Barclay", "Bartolo", "Bellamy", "Belle", "Benjamin"],
        C: [
          "Caiden",
          "Calvin",
          "Candy",
          "Carl",
          "Cherilyn",
          "Chester",
          "Chloe"
        ],
        V: ["Vladimir", "Hehe"]
      }
    };
  }
};
</script>
<style scoped>
.subnavbar {
  /* background-color: #fff; */
  /* color: #000; */
}
</style>


