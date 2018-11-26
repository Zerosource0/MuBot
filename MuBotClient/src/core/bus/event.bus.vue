<script lang="ts">
    import Vue from "vue";
    import { Component } from "vue-property-decorator";

    @Component
    export default class EventBus extends Vue {
        ServerValidationError = "ServerValidationError";

        serverValidationError(data: object) {
            this.$emit(this.ServerValidationError, data);
        }

        onServerValidationError(callback: (data: object) => void) {
            this.$on(this.ServerValidationError, callback);
        }
    }
</script>
