
// Import Swiper and modules
import Swiper from 'swiper';
import { Navigation, Pagination, Scrollbar } from 'swiper/modules';

const swiper = new Swiper('.swiper', {
    coverflow: {
        depth: 100,
        modifier: 1,
        rotate: 50,
        scale: 1,
        slideShadows: true,
        stretch: 0,
    },
});
