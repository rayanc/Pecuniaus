<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
      xmlns:ms="urn:schemas-microsoft-com:xslt"
      xmlns:dt="urn:schemas-microsoft-com:datatypes">
  
  <xsl:template match="/ContractBLA">
    <html>
      <head>
        <title></title>
      </head>
      <body style="font-size: 9px;line-height:95%;text-align: justify">
        <div>
          <p align="center">
            <strong>ACUERDO DE AVANCE DE EFECTIVO COMERCIAL Avanzame</strong>
          </p>
          <p>
            Acuerdo # <xsl:value-of select="ContractNumber" /> Nombre del representante de venta: <xsl:value-of select="SalesRepName" /> Cédula # <xsl:value-of select="SalesRepId" />
          <br />
            Este Acuerdo de Avance de Efectivo Comercial (“<u>Acuerdo”</u>) de fecha
           <xsl:value-of select="ms:format-date(Date, 'dd/MM/yyyy')"/>
            <!--(día)/____(mes)/____(año)--> entre <strong>Avanzame,</strong>, (<u>“Comprador”</u>) y el comercio u otro negocio señalado a continuación (<u>“Vendedor”</u>).
          </p>
          <br />
          <p align="center">
            <strong>INFORMACIÓN DEL VENDEDOR</strong>
          </p>
          <br />
          <p>
            Nombre o razón social: <xsl:value-of select="CompanyName" />
          </p>
          <p>
            Nombre Comercial: <xsl:value-of select="BusinessName" /> País de incorporación/organización ___________________
          </p>
          <p>
            Tipo de entidad (elija una):

            <xsl:for-each select="EntityList/EntityType">
              <!--✓ ▭-->
              [ <xsl:if test="/ContractBLA/BusinessTypeId = EntityTypeId">
                X <!--✅ &#10004; &#10003; Fix required tfor unicode support-->
              </xsl:if> ]
              <xsl:value-of select="EntityName" />

            </xsl:for-each>

            <!--Sociedad Anónima Sociedad Anónima Simplificada Sociedad
            de Responsabilidad Limitada S Sociedad en Nombre Colectivo Sociedad en Comandita Simple Sociedad en Comandita por Acciones Empresa Individual de
            Responsabilidad Limitada Otra-->
          </p>
          <p>
            Dirección Jurídica: __<xsl:value-of select="Address" />__   Provincia: __<xsl:value-of select="Province" />__ País: __<xsl:value-of select="Country" />__
          </p>
          <p>
            Dirección Física: __<xsl:value-of select="LegalAddress" />__ Provincia: __<xsl:value-of select="LegalProvince" />__ País: __<xsl:value-of select="LegalCountry" />__
          </p>
          <p>
            Fecha en que inició el negocio (mm/aa):__<xsl:value-of select="ms:format-date(BusinessStartDate, 'dd/MM/yyyy')" />__ RNC #: __<xsl:value-of select="RNCNumber" />__
          </p>
          <p>
            Nombre de contacto: __<xsl:value-of select="OwnerList/OwnerList[1]/Name" />__ Cedula: __<xsl:value-of select="OwnerList/OwnerList[1]/PassportNbr" />__Cargo: _________________________
          </p>
          <p>Dirección del contacto: __<xsl:value-of select="OwnerList/OwnerList[1]/Address/addressLine1" />__ Provincia: __<xsl:value-of select="OwnerList/OwnerList[1]/Address/state" />__ País: __<xsl:value-of select="OwnerList/OwnerList[1]/Address/CountryName" />__</p>
          <p>
            Teléfono: __<xsl:value-of select="OwnerList/OwnerList[1]/Address/phone1" />__ Fax: __<xsl:value-of select="OwnerList/OwnerList[1]/Address/fax" />__ Correo electrónico: __<xsl:value-of select="OwnerList/OwnerList[1]/Address/email" />__
          </p>
          <!--<p>
            Página de Internet: _____________________________________________
          </p>
          <p>
            Nombre del Banco: __<xsl:value-of select="BankName" />__ Ciudad: __<xsl:value-of select="BankCity" />__ País: __<xsl:value-of select="BankCountry" />__
          </p>-->
          <br />
          <p align="center">
            <strong>AVANCE DE EFECTIVO COMERCIAL</strong>
          </p>
          <p>
            A cambio de la realización por parte del Comprador del monto de avance de Efectivo comercial especificado abajo a favor del Vendedor 
            (“Monto de Avance de Efectivo Comercial”), el Comprador por este medio le compra al Vendedor y el Vendedor por este medio le vende al Comprador todos los derechos, títulos e intereses del Vendedor al monto a pagar abajo 
            (“<u>Monto a Pagar</u>”) de los pagos futuros del Vendedor (“<u>Pagos futuros</u>”) que provienen de los pagos que realizan los clientes del Vendedor con tarjetas de créditos, 
            tarjetas de débito, tarjetas de cargos, tarjetas bancarias y otras tarjetas de pago (“<u>Tarjetas</u>”) de los tipos acordados, directamente o indirectamente por el Procesador (como se define abajo). 
            El Vendedor enviará el Monto a Pagar al Comprador, para que de esta forma un procesador de tarjetas de crédito que haya sido aprobado por el Comprador al momento de la suscripción del presente acuerdo o en cualquier tiempo futuro durante la vigencia del mismo (“<u>Procesador</u>”) para pagarle al mismo un monto 
            de efectivo diario equivalente al porcentaje a pagar especificado abajo (“<u>Porcentaje a Pagar</u>”) de todos los pagos por tarjeta que de otro modo se le deben al Vendedor en el día en cuestión (“<u>Pagos</u>”). 
            El Vendedor continuará asegurándose de que se le pague al Comprador el Porcentaje a Pagar del pago respectivo hasta que el Vendedor haya remitido al Comprador el Monto a pagar de los pagos futuros en su totalidad.
           </p>
          <p>
            Monto de Avance de Efectivo Comercial: = $ __<xsl:value-of select="McaAmount" />__ Monto a Pagar: = $
            __<xsl:value-of select="TotalOwnedAount" />__ Porcentaje a Pagar: = % __<xsl:value-of select="Retention" />__
          </p>
          <p>
            El Comprador no aumentará el Porcentaje a Pagar sin el consentimiento previo del Vendedor. El vendedor (i) suscribirá un contrato con el/los Procesador(es), que sea aprobado por el Comprador (“ <u>Acuerdo de Procesamiento</u>”) para obtener servicios de procesamiento y (ii) por la presente autoriza e instruye al Procesador y Operador (como se define abajo) a pagar diariamente al Comprador el efectivo atribuible al Porcentaje a Pagar de los pagos respectivos en lugar de hacerlo al Vendedor y también a debitar o retirar tales montos de la cuenta del Vendedor, incluyendo la Cuenta (como se define abajo), hasta que el Comprador reciba el efectivo correspondiente al Monto a Pagar en su totalidad de los Pagos Futuros.
          </p>
          <br />
          <p  >
            <strong>
              Gastos Administrativos/ Gastos de Cierre. El Vendedor asumirá los gastos administrativos y gastos de cierre ascendentes a la suma de $__<xsl:value-of select="ExpenseAmount" />
              , que incluye la entrega de estados financieros, y además proporcionar los estados de cuenta mensuales, y brindar acceso en línea a la información de la cuenta así como el acceso telefónico gratuito a los representantes de servicio al cliente. El Comprador puede cobrar montos adicionales por gastos de copias y otra documentación que el Vendedor solicite ocasionalmente (una lista de los cargos adicionales estará disponible en línea o cuando se solicite). El Vendedor acepta y autoriza que el Comprador y las personas designadas puedan debitar o no retirar tales gastos y cargos de las cuentas del Vendedor, incluyendo la Cuenta.
            </strong>
          </p>
          <br />
          <br clear="ALL" />
          <p align="center">
            <strong>GARANTÍAS PERSONALES DE LOS DIRECTORES</strong>
          </p>
          <p>
            COMO CONSIDERACIÓN PARA EL COMPRADOR QUE SE SUSCRIBE A ESTE CONTRATO, EL PROPIETARIO SUSCRIBIENTE (SEA ACCIONISTA, SOCIO, MIEMBRO U OTRO PROPIETARIO), DIRECTOR(ES), OFICIAL(ES) U OTROS REPRESENTANTE(ES) DEL VENDEDOR (COLECTIVAMENTE, “DIRECTOR(ES)”) POR LA PRESENTE, DE FORMA PERSONAL E INCONDICIONAL GARANTIZAN AL COMPRADOR Y SUS CESIONARIOS QUE (I) TODA LA INFORMACIÓN PROVISTA AL COMPRADOR EN CONEXIÓN CON ESTE ACUERDO Y LAS
            TRANSACCIONES QUE CONTEMPLA SON VERDADERAS, FIELES Y COMPLETAS EN TODOS LOS ASPECTOS, (II) EL VENDEDOR NO PODRÁ REEMPLAZAR EL PROCESADOR ACEPTADO POR EL COMPRADOR O PARTICIPAR O UTILIZAR LOS SERVICIOS DE CUALQUIER OTRO
            PROCESADOR ADICIONAL ANTES QUE EL COMPRADOR HAYA RECIBIDO EL MONTO A PAGAR COMPLETO DE AVANCE DE EFECTIVO COMERCIAL, Y (III) EL VENDEDOR NO INCUMPLIRÁ NINGUNA DE LAS “DISPOSICIONES CONTRACTUALES DEL VENDEDOR” COMO SE
            DEFINE EN LA SECCIÓN 2.1 DEL MISMO. En adición, el/los Director(es) garantizan y acuerdan pagar al Comprador y sus cesionarios todos los montos que el Comprador tenga el derecho a recibir conforme y de acuerdo con la Sección 3.3 (Recursos) y Sección 4.8
            (Montos Indemnizados) del mismo. Las obligaciones de los Directores de acuerdo a las garantías anteriores son primordiales, conjuntas o individuales, e irrevocables, independientemente de la autenticidad, validez, regularidad o ejecución de este Contrato, y
            sin tomar en consideración cualquier circunstancia que podrá constituir la exoneración legal o equitativa del fiador. Todos los Directores renuncian a cualquier derecho de exigirle al Comprador ser el primero en proceder en contra del Vendedor o cualquier
            otro de los Directores o agotar primeramente cualquier recurso en contra del Vendedor, los Pagos Futuros o cualquier propiedad sujeta a la Sección 3.5 del mismo antes de proceder en contra de tales Directores, y por tanto renuncian de manera expresa al
            derecho de excusión. Los Directores acuerdan y por la presente hacen cada uno su declaración y garantía establecida en las Secciones 2 y 3 de este Acuerdo (y cada uno de los subcapítulos del mismo) cuya representación y garantía estarán vigentes a la terminación de
            este Acuerdo según lo previsto en la Sección 4.9 del mismo.
          </p>
          <br clear="ALL" />
          <p align="center">
            <strong>AUTORIZACIÓN Y ACEPTACIÓN</strong>
          </p>
          <p>
            En la medida que se establece en el presente documento, cada uno de los infrascritos estará obligado a cumplir los términos de este Acuerdo, incluyendo los Términos Adicionales que se establecen en las páginas siguientes. La persona que suscriba este Acuerdo a
            nombre del Vendedor declara y garantiza que él/ella está autorizado para hacerlo y obliga al Vendedor a cumplir todos los términos y condiciones previstas en el presente documento. Cada uno de los infrascritos declara y garantiza que la información establecida en el
            presente documento y cada declaración del Comprador es verdadera, fiel y completa en todo los aspectos. Si cualquiera de estas informaciones es falsa o errónea, el Vendedor se considerará en incumplimiento material de todos los acuerdos entre el Vendedor y
            el Comprador y el Comprador tendrá el derecho a todos los recursos disponibles bajo la ley. Una Investigación o reporte de consumidor puede hacerse en conexión con este Contrato. El Vendedor y cada uno de los Directores autorizados por el Comprador,
            sus agentes y representantes y cualquier agencia de reporte de crédito contratados por el Comprador, podrán (i) investigar cualquier referencia dada o cualquier otro estado de cuentas o información obtenida del o sobre el Vendedor o cualquiera de los Directores
            para el propósito de este Contrato, y (ii) hacer cualquier reporte de crédito a cualquier hora desde ahora o por tanto tiempo mientras que el Vendedor o los Directores continúen teniendo cualquier obligación frente al Comprador como consecuencia de este Acuerdo
            por la capacidad del Comprador de determinar la elegibilidad del Vendedor de acordar cualquier Acuerdo futuro con el Comprador. Los pagos del Comprador del Precio de Compra se considerarán aceptados y realizados por el Comprador de este Contrato, no
            obstante el Comprador no ejecute este Contrato.
          </p>
          <p>
            Por el Vendedor
          </p>
          <p>
            <table>
              <xsl:for-each select="OwnerList/OwnerList">
                <tr>
                  <td align="center">
                    __________________________________<br />
                    <!--(<xsl:value-of select="Name" />)--> (Nombre)
                  </td>
                  <td align="center">
                    __________________________________<br />
                    (Firma)
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </p>
          <p>
            Por el Comprador
          </p>
          <p>
            <table>
              <tr>
                <td align="center">
                  __________________________________<br />
                  (Nombre)
                </td>
                <td align="center">
                  __________________________________<br />
                  (Firma)
                </td>
              </tr>
            </table>
          </p>
          <br clear="all" />
        </div>
        <u>
          <br clear="all" />
        </u>

      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

