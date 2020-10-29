<template>
    <div>
        <h2 ><b-badge>{{ msg }}</b-badge></h2>
    </div>
</template>

<script>
    export default {
        name: 'Title',
        props: {
            msg: String
        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

h2 {
    text-align: center;
}

</style>

