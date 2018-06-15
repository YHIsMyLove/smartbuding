<template>
    <f7-page>
        <f7-navbar title="大事件" back-link="Back"></f7-navbar>
        <div class="timeline timeline-sides">
        <div class="timeline-item">
            <div class="timeline-item-date">21 <small>DEC</small></div>
            <div class="timeline-item-divider"></div>
            <div class="timeline-item-content">
            <div class="timeline-item-inner">Some text goes here</div>
            </div>
        </div>
        <div class="timeline-item">
            <div class="timeline-item-date">22 <small>DEC</small></div>
            <div class="timeline-item-divider"></div>
            <div class="timeline-item-content">
            <div class="timeline-item-inner">Another text goes here</div>
            </div>
        </div>
        <div class="timeline-item">
            <div class="timeline-item-date">23 <small>DEC</small></div>
            <div class="timeline-item-divider"></div>
            <div class="timeline-item-content">
            <div class="timeline-item-inner">Just plain text</div>
            </div>
        </div>
        <div class="timeline-item">
            <div class="timeline-item-date">24 <small>DEC</small></div>
            <div class="timeline-item-divider"></div>
            <div class="timeline-item-content">
            <div class="timeline-item-inner">One more text here</div>
            </div>
        </div>
        </div>
    </f7-page>
</template>
<script>
  export default {
    data: function () {
      return {
        items: (function () {
          var it = [];
          for (var i = 0; i < 100; i++) it.push(i+1);
          return it;
        })()
      }
    },
    methods: {
      onSearch: function (searchbar, query, previousQuery) {
        console.log('search', query);
      },
      onClear: function (event) {
        console.log('clear');
      },
      onEnable: function (event) {
        console.log('enable');
      },
      onDisable: function (event) {
        console.log('disable');
      },
      enableSearchbar(){
          this.$f7.searchbar.enable('#searchbar');
      }
    }
  }
</script>
<style>
</style>

