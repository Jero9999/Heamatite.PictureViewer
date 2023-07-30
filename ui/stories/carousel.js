import { html } from 'lit';
import '../components/carousel';
import capral from './assets/images/capral.png'

const imageList = Array(51).fill(capral);
//export const Carousel = () => html`<motion-carousel ></motion-carousel>`;
export const Carousel = () => html`<motion-carousel .data=${imageList}></motion-carousel>`;