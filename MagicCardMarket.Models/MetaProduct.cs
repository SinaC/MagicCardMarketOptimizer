﻿using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:Metaproduct
    [Serializable]
    [XmlType("metaproduct", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "metaproduct", IsNullable = false)]
    public class MetaProduct
    {
        [XmlElement("idMetaproduct", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public MetaProductName[] Names { get; set; }

        [XmlElement("products", Form = XmlSchemaForm.Unqualified)]
        public MetaProductIds Products { get; set; }
    }
    //<metaproduct>
    //  <idMetaproduct>2923</idMetaproduct>
    //  <name>
    //    <idLanguage>1</idLanguage>
    //    <languageName>English</languageName>
    //    <metaproductName>Island</metaproductName>
    //  </name>
    //  <name>
    //    <idLanguage>2</idLanguage>
    //    <languageName>French</languageName>
    //    <metaproductName>Île</metaproductName>
    //  </name>
    //  <name>
    //    <idLanguage>3</idLanguage>
    //    <languageName>German</languageName>
    //    <metaproductName>Insel</metaproductName>
    //  </name>
    //  <name>
    //    <idLanguage>4</idLanguage>
    //    <languageName>Spanish</languageName>
    //    <metaproductName>Isla</metaproductName>
    //  </name>
    //  <name>
    //    <idLanguage>5</idLanguage>
    //    <languageName>Italian</languageName>
    //    <metaproductName>Isola</metaproductName>
    //  </name>
    //  <products>
    //    <idProduct>289021</idProduct>
    //    <idProduct>289020</idProduct>
    //    <idProduct>289019</idProduct>
    //    <idProduct>288609</idProduct>
    //    <idProduct>288608</idProduct>
    //    <idProduct>288607</idProduct>
    //    <idProduct>288603</idProduct>
    //    <idProduct>288602</idProduct>
    //    <idProduct>288601</idProduct>
    //    <idProduct>286090</idProduct>
    //    <idProduct>286089</idProduct>
    //    <idProduct>286088</idProduct>
    //    <idProduct>286087</idProduct>
    //    <idProduct>284936</idProduct>
    //    <idProduct>284935</idProduct>
    //    <idProduct>284934</idProduct>
    //    <idProduct>284933</idProduct>
    //    <idProduct>284932</idProduct>
    //    <idProduct>284398</idProduct>
    //    <idProduct>284397</idProduct>
    //    <idProduct>284383</idProduct>
    //    <idProduct>284382</idProduct>
    //    <idProduct>284320</idProduct>
    //    <idProduct>283481</idProduct>
    //    <idProduct>283480</idProduct>
    //    <idProduct>283479</idProduct>
    //    <idProduct>283478</idProduct>
    //    <idProduct>273240</idProduct>
    //    <idProduct>273239</idProduct>
    //    <idProduct>273238</idProduct>
    //    <idProduct>273026</idProduct>
    //    <idProduct>273025</idProduct>
    //    <idProduct>273020</idProduct>
    //    <idProduct>272957</idProduct>
    //    <idProduct>272875</idProduct>
    //    <idProduct>272659</idProduct>
    //    <idProduct>271550</idProduct>
    //    <idProduct>271549</idProduct>
    //    <idProduct>270769</idProduct>
    //    <idProduct>270768</idProduct>
    //    <idProduct>270767</idProduct>
    //    <idProduct>270766</idProduct>
    //    <idProduct>270028</idProduct>
    //    <idProduct>270027</idProduct>
    //    <idProduct>270026</idProduct>
    //    <idProduct>270025</idProduct>
    //    <idProduct>269229</idProduct>
    //    <idProduct>269228</idProduct>
    //    <idProduct>269167</idProduct>
    //    <idProduct>269166</idProduct>
    //    <idProduct>269165</idProduct>
    //    <idProduct>269164</idProduct>
    //    <idProduct>268855</idProduct>
    //    <idProduct>268631</idProduct>
    //    <idProduct>267714</idProduct>
    //    <idProduct>267713</idProduct>
    //    <idProduct>267712</idProduct>
    //    <idProduct>267711</idProduct>
    //    <idProduct>266361</idProduct>
    //    <idProduct>266360</idProduct>
    //    <idProduct>266359</idProduct>
    //    <idProduct>266358</idProduct>
    //    <idProduct>266357</idProduct>
    //    <idProduct>265022</idProduct>
    //    <idProduct>265021</idProduct>
    //    <idProduct>265020</idProduct>
    //    <idProduct>265019</idProduct>
    //    <idProduct>263923</idProduct>
    //    <idProduct>263922</idProduct>
    //    <idProduct>263921</idProduct>
    //    <idProduct>263920</idProduct>
    //    <idProduct>262389</idProduct>
    //    <idProduct>262388</idProduct>
    //    <idProduct>262387</idProduct>
    //    <idProduct>262386</idProduct>
    //    <idProduct>258337</idProduct>
    //    <idProduct>258336</idProduct>
    //    <idProduct>258335</idProduct>
    //    <idProduct>258334</idProduct>
    //    <idProduct>258244</idProduct>
    //    <idProduct>258243</idProduct>
    //    <idProduct>258242</idProduct>
    //    <idProduct>258241</idProduct>
    //    <idProduct>258240</idProduct>
    //    <idProduct>257605</idProduct>
    //    <idProduct>257604</idProduct>
    //    <idProduct>257603</idProduct>
    //    <idProduct>257602</idProduct>
    //    <idProduct>257590</idProduct>
    //    <idProduct>256406</idProduct>
    //    <idProduct>256405</idProduct>
    //    <idProduct>256404</idProduct>
    //    <idProduct>256403</idProduct>
    //    <idProduct>256336</idProduct>
    //    <idProduct>256335</idProduct>
    //    <idProduct>256334</idProduct>
    //    <idProduct>256333</idProduct>
    //    <idProduct>256325</idProduct>
    //    <idProduct>254205</idProduct>
    //    <idProduct>254204</idProduct>
    //    <idProduct>254203</idProduct>
    //    <idProduct>254090</idProduct>
    //    <idProduct>254089</idProduct>
    //    <idProduct>254088</idProduct>
    //    <idProduct>254087</idProduct>
    //    <idProduct>253586</idProduct>
    //    <idProduct>253585</idProduct>
    //    <idProduct>253584</idProduct>
    //    <idProduct>253489</idProduct>
    //    <idProduct>253415</idProduct>
    //    <idProduct>251453</idProduct>
    //    <idProduct>251285</idProduct>
    //    <idProduct>251284</idProduct>
    //    <idProduct>250491</idProduct>
    //    <idProduct>250370</idProduct>
    //    <idProduct>250369</idProduct>
    //    <idProduct>250368</idProduct>
    //    <idProduct>250140</idProduct>
    //    <idProduct>250139</idProduct>
    //    <idProduct>250138</idProduct>
    //    <idProduct>250137</idProduct>
    //    <idProduct>250133</idProduct>
    //    <idProduct>250132</idProduct>
    //    <idProduct>250131</idProduct>
    //    <idProduct>250130</idProduct>
    //    <idProduct>250129</idProduct>
    //    <idProduct>250128</idProduct>
    //    <idProduct>250127</idProduct>
    //    <idProduct>250126</idProduct>
    //    <idProduct>250123</idProduct>
    //    <idProduct>250107</idProduct>
    //    <idProduct>250106</idProduct>
    //    <idProduct>250105</idProduct>
    //    <idProduct>250104</idProduct>
    //    <idProduct>250060</idProduct>
    //    <idProduct>250059</idProduct>
    //    <idProduct>250058</idProduct>
    //    <idProduct>250057</idProduct>
    //    <idProduct>250056</idProduct>
    //    <idProduct>250055</idProduct>
    //    <idProduct>250054</idProduct>
    //    <idProduct>250053</idProduct>
    //    <idProduct>250052</idProduct>
    //    <idProduct>250051</idProduct>
    //    <idProduct>250050</idProduct>
    //    <idProduct>250045</idProduct>
    //    <idProduct>249900</idProduct>
    //    <idProduct>249899</idProduct>
    //    <idProduct>249898</idProduct>
    //    <idProduct>249897</idProduct>
    //    <idProduct>249593</idProduct>
    //    <idProduct>249537</idProduct>
    //    <idProduct>249536</idProduct>
    //    <idProduct>249535</idProduct>
    //    <idProduct>249534</idProduct>
    //    <idProduct>249512</idProduct>
    //    <idProduct>249511</idProduct>
    //    <idProduct>249510</idProduct>
    //    <idProduct>249509</idProduct>
    //    <idProduct>249460</idProduct>
    //    <idProduct>249459</idProduct>
    //    <idProduct>249458</idProduct>
    //    <idProduct>249457</idProduct>
    //    <idProduct>249434</idProduct>
    //    <idProduct>249433</idProduct>
    //    <idProduct>249432</idProduct>
    //    <idProduct>249431</idProduct>
    //    <idProduct>249360</idProduct>
    //    <idProduct>249359</idProduct>
    //    <idProduct>249358</idProduct>
    //    <idProduct>249357</idProduct>
    //    <idProduct>249268</idProduct>
    //    <idProduct>249267</idProduct>
    //    <idProduct>249266</idProduct>
    //    <idProduct>249228</idProduct>
    //    <idProduct>249227</idProduct>
    //    <idProduct>249226</idProduct>
    //    <idProduct>249193</idProduct>
    //    <idProduct>249192</idProduct>
    //    <idProduct>249191</idProduct>
    //    <idProduct>248043</idProduct>
    //    <idProduct>248042</idProduct>
    //    <idProduct>248041</idProduct>
    //    <idProduct>248040</idProduct>
    //    <idProduct>247404</idProduct>
    //    <idProduct>247399</idProduct>
    //    <idProduct>247394</idProduct>
    //    <idProduct>247260</idProduct>
    //    <idProduct>246667</idProduct>
    //    <idProduct>245832</idProduct>
    //    <idProduct>245831</idProduct>
    //    <idProduct>245456</idProduct>
    //    <idProduct>245455</idProduct>
    //    <idProduct>242556</idProduct>
    //    <idProduct>242555</idProduct>
    //    <idProduct>242554</idProduct>
    //    <idProduct>242553</idProduct>
    //    <idProduct>242488</idProduct>
    //    <idProduct>242487</idProduct>
    //    <idProduct>242486</idProduct>
    //    <idProduct>242485</idProduct>
    //    <idProduct>242130</idProduct>
    //    <idProduct>242129</idProduct>
    //    <idProduct>242128</idProduct>
    //    <idProduct>242116</idProduct>
    //    <idProduct>242003</idProduct>
    //    <idProduct>242002</idProduct>
    //    <idProduct>242001</idProduct>
    //    <idProduct>242000</idProduct>
    //    <idProduct>241893</idProduct>
    //    <idProduct>241892</idProduct>
    //    <idProduct>241891</idProduct>
    //    <idProduct>241069</idProduct>
    //    <idProduct>241068</idProduct>
    //    <idProduct>241067</idProduct>
    //    <idProduct>241066</idProduct>
    //    <idProduct>22910</idProduct>
    //    <idProduct>22909</idProduct>
    //    <idProduct>22908</idProduct>
    //    <idProduct>22833</idProduct>
    //    <idProduct>22832</idProduct>
    //    <idProduct>22831</idProduct>
    //    <idProduct>22391</idProduct>
    //    <idProduct>22390</idProduct>
    //    <idProduct>22389</idProduct>
    //    <idProduct>22388</idProduct>
    //    <idProduct>22339</idProduct>
    //    <idProduct>22086</idProduct>
    //    <idProduct>21699</idProduct>
    //    <idProduct>21698</idProduct>
    //    <idProduct>21697</idProduct>
    //    <idProduct>21696</idProduct>
    //    <idProduct>21695</idProduct>
    //    <idProduct>21694</idProduct>
    //    <idProduct>21693</idProduct>
    //    <idProduct>21692</idProduct>
    //    <idProduct>21664</idProduct>
    //    <idProduct>21663</idProduct>
    //    <idProduct>21662</idProduct>
    //    <idProduct>21661</idProduct>
    //    <idProduct>21347</idProduct>
    //    <idProduct>21283</idProduct>
    //    <idProduct>21282</idProduct>
    //    <idProduct>21281</idProduct>
    //    <idProduct>21280</idProduct>
    //    <idProduct>21043</idProduct>
    //    <idProduct>21030</idProduct>
    //    <idProduct>20291</idProduct>
    //    <idProduct>20290</idProduct>
    //    <idProduct>20289</idProduct>
    //    <idProduct>20288</idProduct>
    //    <idProduct>20287</idProduct>
    //    <idProduct>20286</idProduct>
    //    <idProduct>20285</idProduct>
    //    <idProduct>20284</idProduct>
    //    <idProduct>20283</idProduct>
    //    <idProduct>20282</idProduct>
    //    <idProduct>20281</idProduct>
    //    <idProduct>20280</idProduct>
    //    <idProduct>20279</idProduct>
    //    <idProduct>20278</idProduct>
    //    <idProduct>20277</idProduct>
    //    <idProduct>20276</idProduct>
    //    <idProduct>20275</idProduct>
    //    <idProduct>20274</idProduct>
    //    <idProduct>20273</idProduct>
    //    <idProduct>20272</idProduct>
    //    <idProduct>20271</idProduct>
    //    <idProduct>20270</idProduct>
    //    <idProduct>20269</idProduct>
    //    <idProduct>20268</idProduct>
    //    <idProduct>20267</idProduct>
    //    <idProduct>20266</idProduct>
    //    <idProduct>20265</idProduct>
    //    <idProduct>20264</idProduct>
    //    <idProduct>20263</idProduct>
    //    <idProduct>20262</idProduct>
    //    <idProduct>20261</idProduct>
    //    <idProduct>20260</idProduct>
    //    <idProduct>20259</idProduct>
    //    <idProduct>20258</idProduct>
    //    <idProduct>20257</idProduct>
    //    <idProduct>20256</idProduct>
    //    <idProduct>20255</idProduct>
    //    <idProduct>20254</idProduct>
    //    <idProduct>20253</idProduct>
    //    <idProduct>20252</idProduct>
    //    <idProduct>20251</idProduct>
    //    <idProduct>20250</idProduct>
    //    <idProduct>20249</idProduct>
    //    <idProduct>20248</idProduct>
    //    <idProduct>20247</idProduct>
    //    <idProduct>20246</idProduct>
    //    <idProduct>20010</idProduct>
    //    <idProduct>20009</idProduct>
    //    <idProduct>20008</idProduct>
    //    <idProduct>20007</idProduct>
    //    <idProduct>19760</idProduct>
    //    <idProduct>19759</idProduct>
    //    <idProduct>19758</idProduct>
    //    <idProduct>19757</idProduct>
    //    <idProduct>19681</idProduct>
    //    <idProduct>19680</idProduct>
    //    <idProduct>19679</idProduct>
    //    <idProduct>19678</idProduct>
    //    <idProduct>19677</idProduct>
    //    <idProduct>19336</idProduct>
    //    <idProduct>19335</idProduct>
    //    <idProduct>19334</idProduct>
    //    <idProduct>19303</idProduct>
    //    <idProduct>19302</idProduct>
    //    <idProduct>19301</idProduct>
    //    <idProduct>19300</idProduct>
    //    <idProduct>18275</idProduct>
    //    <idProduct>18237</idProduct>
    //    <idProduct>18236</idProduct>
    //    <idProduct>18235</idProduct>
    //    <idProduct>18234</idProduct>
    //    <idProduct>18233</idProduct>
    //    <idProduct>18232</idProduct>
    //    <idProduct>18231</idProduct>
    //    <idProduct>18230</idProduct>
    //    <idProduct>18193</idProduct>
    //    <idProduct>18071</idProduct>
    //    <idProduct>18070</idProduct>
    //    <idProduct>18069</idProduct>
    //    <idProduct>18030</idProduct>
    //    <idProduct>18029</idProduct>
    //    <idProduct>18028</idProduct>
    //    <idProduct>18027</idProduct>
    //    <idProduct>17685</idProduct>
    //    <idProduct>17684</idProduct>
    //    <idProduct>17683</idProduct>
    //    <idProduct>17623</idProduct>
    //    <idProduct>17622</idProduct>
    //    <idProduct>17621</idProduct>
    //    <idProduct>17550</idProduct>
    //    <idProduct>17545</idProduct>
    //    <idProduct>17540</idProduct>
    //    <idProduct>17248</idProduct>
    //    <idProduct>17243</idProduct>
    //    <idProduct>17238</idProduct>
    //    <idProduct>16945</idProduct>
    //    <idProduct>16940</idProduct>
    //    <idProduct>16935</idProduct>
    //    <idProduct>16535</idProduct>
    //    <idProduct>16534</idProduct>
    //    <idProduct>16533</idProduct>
    //    <idProduct>16532</idProduct>
    //    <idProduct>16161</idProduct>
    //    <idProduct>16156</idProduct>
    //    <idProduct>16151</idProduct>
    //    <idProduct>15466</idProduct>
    //    <idProduct>15465</idProduct>
    //    <idProduct>15464</idProduct>
    //    <idProduct>14900</idProduct>
    //    <idProduct>14721</idProduct>
    //    <idProduct>14720</idProduct>
    //    <idProduct>14719</idProduct>
    //    <idProduct>14718</idProduct>
    //    <idProduct>14717</idProduct>
    //    <idProduct>14595</idProduct>
    //    <idProduct>14594</idProduct>
    //    <idProduct>14593</idProduct>
    //    <idProduct>14592</idProduct>
    //    <idProduct>14425</idProduct>
    //    <idProduct>14424</idProduct>
    //    <idProduct>14423</idProduct>
    //    <idProduct>13879</idProduct>
    //    <idProduct>13878</idProduct>
    //    <idProduct>13877</idProduct>
    //    <idProduct>13876</idProduct>
    //    <idProduct>13442</idProduct>
    //    <idProduct>13441</idProduct>
    //    <idProduct>13440</idProduct>
    //    <idProduct>13439</idProduct>
    //    <idProduct>12413</idProduct>
    //    <idProduct>12412</idProduct>
    //    <idProduct>12411</idProduct>
    //    <idProduct>12410</idProduct>
    //    <idProduct>12260</idProduct>
    //    <idProduct>12259</idProduct>
    //    <idProduct>12258</idProduct>
    //    <idProduct>12060</idProduct>
    //    <idProduct>11914</idProduct>
    //    <idProduct>11711</idProduct>
    //    <idProduct>11710</idProduct>
    //    <idProduct>11709</idProduct>
    //    <idProduct>11708</idProduct>
    //    <idProduct>11364</idProduct>
    //    <idProduct>11363</idProduct>
    //    <idProduct>11362</idProduct>
    //    <idProduct>11181</idProduct>
    //    <idProduct>11180</idProduct>
    //    <idProduct>11179</idProduct>
    //    <idProduct>11178</idProduct>
    //    <idProduct>10545</idProduct>
    //    <idProduct>10544</idProduct>
    //    <idProduct>10543</idProduct>
    //    <idProduct>10542</idProduct>
    //    <idProduct>10198</idProduct>
    //    <idProduct>10197</idProduct>
    //    <idProduct>10196</idProduct>
    //    <idProduct>10189</idProduct>
    //    <idProduct>9976</idProduct>
    //    <idProduct>9975</idProduct>
    //    <idProduct>9974</idProduct>
    //    <idProduct>9799</idProduct>
    //    <idProduct>9798</idProduct>
    //    <idProduct>9797</idProduct>
    //    <idProduct>9796</idProduct>
    //    <idProduct>9051</idProduct>
    //    <idProduct>9050</idProduct>
    //    <idProduct>9049</idProduct>
    //    <idProduct>9048</idProduct>
    //    <idProduct>8351</idProduct>
    //    <idProduct>8350</idProduct>
    //    <idProduct>8349</idProduct>
    //    <idProduct>8348</idProduct>
    //    <idProduct>6548</idProduct>
    //    <idProduct>6547</idProduct>
    //    <idProduct>6546</idProduct>
    //    <idProduct>6014</idProduct>
    //    <idProduct>6013</idProduct>
    //    <idProduct>6012</idProduct>
    //    <idProduct>5817</idProduct>
    //    <idProduct>5816</idProduct>
    //    <idProduct>5815</idProduct>
    //    <idProduct>5515</idProduct>
    //    <idProduct>5514</idProduct>
    //    <idProduct>5197</idProduct>
    //    <idProduct>5196</idProduct>
    //    <idProduct>5195</idProduct>
    //    <idProduct>5194</idProduct>
    //    <idProduct>5193</idProduct>
    //    <idProduct>5192</idProduct>
    //    <idProduct>3524</idProduct>
    //    <idProduct>3523</idProduct>
    //    <idProduct>3522</idProduct>
    //    <idProduct>3521</idProduct>
    //    <idProduct>3097</idProduct>
    //    <idProduct>3096</idProduct>
    //    <idProduct>3095</idProduct>
    //    <idProduct>3094</idProduct>
    //    <idProduct>2750</idProduct>
    //    <idProduct>2749</idProduct>
    //    <idProduct>2748</idProduct>
    //    <idProduct>2747</idProduct>
    //    <idProduct>1969</idProduct>
    //    <idProduct>1968</idProduct>
    //    <idProduct>1967</idProduct>
    //    <idProduct>1966</idProduct>
    //    <idProduct>974</idProduct>
    //    <idProduct>973</idProduct>
    //    <idProduct>972</idProduct>
    //    <idProduct>971</idProduct>
    //    <idProduct>294</idProduct>
    //    <idProduct>293</idProduct>
    //    <idProduct>292</idProduct>
    //    <idProduct>291</idProduct>
    //  </products>
    //</metaproduct>
}
