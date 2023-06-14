<template>
    <div class="pb-6 pb-8 pt-5 pt-md-8 bg-gradient-success">
        <v-containe fluid="true">
            <v-row justify="space-evenly" >
                <v-col class="rounded-lg" cols="3">
                    <v-car>
                        <v-card-item>
                            <h5 class="text-overline cp-title mb-1">
                                TOTAL TRAFFIC
                            </h5>
                            <span class="cp-number mb-1 font-weight-bold">
                                350,897
                            </span>
                            <template v-slot:append>
                                <div>
                                    <v-icon class="me-1 mt-0 text-warning" icon="mdi-heart"></v-icon>
                                </div>
                            </template>
                        </v-card-item>
                        <v-card-item>
                            <div class="text-caption text-dark"><span class="text-success">3.48%</span> Since last month</div>
                        </v-card-item>
                    </v-car>
                </v-col>
                <v-col class="rounded-lg" cols="3">
                    <v-car>
                        <v-card-item>
                            <h5 class="text-overline cp-title mb-1">
                                TOTAL TRAFFIC
                            </h5>
                            <span class="cp-number mb-1 font-weight-bold">
                                350,897
                            </span>
                            <template v-slot:append>
                                <div>
                                    <v-icon class="me-1 mt-0 text-warning" icon="mdi-heart"></v-icon>
                                </div>
                            </template>
                        </v-card-item>
                        <v-card-item>
                            <div class="text-caption text-dark"><span class="text-success">3.48%</span> Since last month</div>
                        </v-card-item>
                    </v-car>
                </v-col>
                <v-col class="rounded-lg" cols="3">
                    <v-car>
                        <v-card-item>
                            <h5 class="text-overline cp-title mb-1">
                                TOTAL TRAFFIC
                            </h5>
                            <span class="cp-number mb-1 font-weight-bold">
                                350,897
                            </span>
                            <template v-slot:append>
                                <div>
                                    <v-icon class="me-1 mt-0 text-warning" icon="mdi-heart"></v-icon>
                                </div>
                            </template>
                        </v-card-item>
                        <v-card-item>
                            <div class="text-caption text-dark"><span class="text-success">3.48%</span> Since last month</div>
                        </v-card-item>
                    </v-car>
                </v-col>
                <v-col class="rounded-lg" cols="3">
                    <v-car>
                        <v-card-item>
                            <h5 class="text-overline cp-title mb-1">
                                TOTAL TRAFFIC
                            </h5>
                            <span class="cp-number mb-1 font-weight-bold">
                                350,897
                            </span>
                            <template v-slot:append>
                                <div>
                                    <v-icon class="me-1 mt-0 text-warning" icon="mdi-heart"></v-icon>
                                </div>
                            </template>
                        </v-card-item>
                        <v-card-item>
                            <div class="text-caption text-dark"><span class="text-success">3.48%</span> Since last month</div>
                        </v-card-item>
                    </v-car>
                </v-col>
            </v-row>
        </v-containe>
    </div>
</template>
<style scoped>
    .bg-gradient-success {
        background: linear-gradient(87deg,#2dce89,#2dcecc);
        height: 40%;
    }

    .rounded-lg {
        background-color: #fff;
        border: 1px solid rgba(0,0,0,.05);
        max-width: 20%;
    }

    .cp-title {
        color: #8898aa;
    }

    .cp-number {
        color: #32325d;
        font-size: 1.2rem;
        font-weight: 600;
    }
</style>
<script>
    export default {
        name: 'Home',
    }
</script>