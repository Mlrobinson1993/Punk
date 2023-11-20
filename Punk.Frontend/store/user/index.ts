import { defineStore } from "pinia";
import { ref } from "vue";
import type { ApiResponse } from "~/types/ApiResponse";
import type { Beer } from "~/types/Beer";
import type { User } from "~/types/User";

export const useUserStore = defineStore(
  "user",
  () => {
    const user: Ref<User | null> = ref(null);

    const getUser = (): User | null => user.value;

    function setUser(userIn: User | null) {
      user.value = userIn;
    }

    const fetchUserFavouriteBeers = async (
      page: number = 1,
      maxResults: number = 25
    ) => {
      const { fetchApi } = useApi();
      try {
        const response: ApiResponse<Beer.Dto[]> = await fetchApi<Beer.Dto[]>(
          `/beer/favourites?page=${page}&pageSize=${maxResults}`
        );

        if (response.statusCode === 200) {
          setUserFavouriteBeers(response.data);
          // return response.data;
        }

        return [];
      } catch (err: any) {
        console.log(err);
        return [];
      }
    };

    function setUserFavouriteBeers(favouriteBeers: User["favouriteBeers"]) {
      if (user.value === null) return;
      user.value.favouriteBeers = favouriteBeers;
    }

    function addUserFavouriteBeer(beer: Beer.Dto) {
      if (user.value === null) return;
      user.value.favouriteBeers.push(beer);
    }

    function removeUserFavouriteBeer(beerId: number) {
      if (user.value === null) return;
      user.value.favouriteBeers = user.value.favouriteBeers.filter(
        (beer) => beer.id !== beerId
      );
    }

    const getUserId = () => user.value?.id ?? "";

    const getUserFavouriteBeers = () => user.value?.favouriteBeers ?? [];

    const userFavouriteBeers = computed(() => {
      return user.value?.favouriteBeers ?? [];
    });

    return {
      user,
      getUser,
      setUser,
      getUserId,
      fetchUserFavouriteBeers,
      getUserFavouriteBeers,
      setUserFavouriteBeers,
      addUserFavouriteBeer,
      removeUserFavouriteBeer,
      userFavouriteBeers,
    };
  },
  {
    persist: {
      storage: persistedState.localStorage,
    },
  }
);
