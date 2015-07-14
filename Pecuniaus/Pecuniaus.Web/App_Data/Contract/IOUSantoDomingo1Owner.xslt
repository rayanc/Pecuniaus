<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
      xmlns:ms="urn:schemas-microsoft-com:xslt"
      xmlns:dt="urn:schemas-microsoft-com:datatypes">

  <xsl:template match="/ContractIOU">
    <html>
      <head>
        <title></title>
      </head>
      <body>
        <div>
          <p>
            <table>
              <tr>
                <td>
                  <b> FOLIO NO ________________</b>
                </td>
                <td align="right" >
                  <b> ACTO NO. __________________</b>
                </td>
              </tr>
            </table>

          </p><br />
          <br />
          <p align="center">
            <b>        PAGARE NOTARIAL</b>
          </p>
          <br />
          <br />
          <p>
            En la ciudad de Santo Domingo de Guzmán, Distrito Nacional, Capital de la República Dominicana, hoy día <xsl:value-of select="LetterDateDay" /> (<xsl:value-of select="ms:format-date(LetterDate, 'dd')" />) del mes de <xsl:value-of select="ms:format-date(LetterDate, 'MMM')" /> del año dos mil <xsl:value-of select="LetterDateYear" /> (20<xsl:value-of select="ms:format-date(LetterDate, 'yy')" />), por ante mí Licdo. RODOLFO ANTONIO GUZMÁN GUZMÁN, __________________________________________Notario Público de los del Número para el Distrito Nacional, portador de la cédula de identidad y electoral No. ____________________, miembro activo del Colegio de Notarios de la República Dominicana, registrado con la Matrícula No. _____________________, con domicilio en la calle Francisco J. Peynado No. 17, Ciudad Nueva, ______________________________Distrito Nacional,
            compareció libre y voluntariamente el señor(a) <xsl:value-of select="AuthorizedOwner/Name" />, <!--<xsl:value-of select="AuthorizedOwner/Name" />,--> mayor de edad, empresario (a), provisto de la Cédula de Identidad y Electoral (Pasaporte) )) <xsl:value-of select="AuthorizedOwner/Id" /> domiciliado y residente en <xsl:value-of select="AuthorizedOwner/Address" />, _____________________, República Dominicana, con domicilio ocasional en la ciudad de Santo Domingo, quien actúa en el presente acto en su doble calidad de representante y fiador solidario de la razón social <xsl:value-of select="LegalCompanyName" />
            , sociedad comercial organizada y existente de conformidad con las leyes de la República Dominicana, titular del Registro Nacional de Contribuyentes <xsl:value-of select="RNC" />, con su domicilio social ubicado en <xsl:value-of select="CompanyAddress" />, <xsl:value-of select="Province" />, República Dominicana; (en lo adelante “La Deudora”); quien me declara lo siguiente: <b>POR CUANTO:</b> Las sociedades <xsl:value-of select="LegalCompanyName" /> y <b>AVANZAME DOMINICANA, LTD.</b>, (en adelante "Avanzame”) una sociedad comercial organizada y existente de conformidad con las leyes de las Islas Vírgenes Británicas, titular del Registro Nacional de Contribuyentes Número 1-3076725-4, con su domicilio social y asiento principal ubicado en el local para oficina en el Segundo Piso de la Plaza Alberto Forrestieri II, en la Avenida Lope de Vega esquina Rafael Augusto Sanchez, de la ciudad de Santo Domingo de Guzmán, Distrito Nacional, Capital de la República Dominicana; suscribieron un Acuerdo de Avance de Efectivo Comercial Avanzame,
            en fecha  <xsl:value-of select="FundingDateDay" /> (<xsl:value-of select="ms:format-date(FundingDate, 'dd')" />) del mes de <xsl:value-of select="ms:format-date(FundingDate, 'MMM')" /> del año dos mil <xsl:value-of select="FundingDateYear" /> (20<xsl:value-of select="ms:format-date(FundingDate, 'yy')" />), (“Acuerdo Avanzame”) mediante el cual la sociedad <xsl:value-of select="LegalCompanyName" />, se constituía en deudora de Avanzame por la suma de <b><xsl:value-of select="TotalOwnedAmountWords" /> (RD$ <xsl:value-of select="TotalOwnedAmount" />)
            POR CUANTO:</b> A la fecha de suscripción del presente pagaré La Deudora declara y reconoceadeudar a Avanzame lasumade <b><xsl:value-of select="TotalOwnedAmountWords" /> (RD$ <xsl:value-of select="TotalOwnedAmount" />) POR CUANTO</b>: La Deudora desea documentar la referida deuda por el presente acto y revestir el derecho a cobro de la misma, con las prerrogativas dispuestas por los artículos 545 y siguientes del Código de Procedimiento Civil de la República Dominicana, en razón de la naturaleza de título ejecutorio del presente pagaré notarial. En tal virtud:<b> PRIMERO:</b> La Deudora se compromete y obliga a pagarle a AVANZAME, oa su orden, la suma <b><xsl:value-of select="TotalOwnedAmountWords" /> (RD$ <xsl:value-of select="TotalOwnedAmount" />)</b> que corresponde al Derecho a Recibir (“DAR”) a la fecha de la suscripción del presente pagaré (en lo adelante “DAR”) de conformidad con los términos previstos en el Acuerdo Avanzame; <b> SEGUNDO:</b> La totalidad del DAR adeudado bajo este Pagaré será repagado a favor de Avanzame a más tardar el día ______________
            (_____) del mes de ______________ del año dos mil ________ (20___) y en manos de Avanzame mediante cheque o transferencia bancaria a la cuenta indicada por Avanzame; <b>TERCERO:</b> En caso de que el DAR adeudado bajo el presente Pagaré no sea pagado por La Deudora en la fecha, en el lugar y de la manera establecidos en el presente Pagaré, La Deudora pagará a Avanzame intereses moratorios a la tasa de un uno por ciento (1%) diario sobre el DAR desde la fecha en que el pago debió haber sido efectuado de conformidad con lo establecido en este Pagaré y hasta la fecha en que el pago sea efectivamente recibido por Avanzame en el lugar y de la manera establecida en este Pagaré; <b>CUARTO</b>: El DAR deberá ser pagado en fondos inmediatamente disponibles y libremente transferibles, sin deducción ni retención alguna por ningún concepto, inclusive por concepto de gravámenes, cargos, impuestos, tasas o contribuciones de cualquier naturaleza, incluyendo, sin limitación, por concepto de retenciones de impuestos aplicables de conformidad con las leyes de la República Dominicana (en adelante las "Deducciones"). En caso de que cualquier pago realizado por La Deudora a Avanzame bajo este Pagaré esté sujeto a Deducciones, La Deudora pagará a Avanzame aquellas cantidades adicionales que sean necesarias para garantizar que las cantidades netas recibidas por Avanzame sean iguales a las cantidades que Avanzame hubiera recibido si dichas Deducciones no hubieran sido efectuadas. En todo caso, si ello fuera requerido de conformidad con la legislación aplicable, La Deudora procederá a entregar los fondos correspondientes a las Deducciones a que hubiera lugar de conformidad con la legislación aplicable, y suministrará a Avanzame, dentro de los treinta (30) días calendario siguientes a su pago, copias de los recibos o comprobantes que evidencien la realización del pago en cumplimiento de la normativa legal aplicable;

            <b>QUINTO</b>: La Sociedad Comercial <xsl:value-of select="LegalCompanyName" />, presenta como Fiador Solidario al señor(a) <xsl:value-of select="AuthorizedOwner/Name" />, quien por medio del presente acto se constituye de manera formal y expresa en Fiador Solidario de la compareciente, quien firma además el presente documento en señal de aprobación; <b>SEXTO</b>: En caso de incumplimiento a una cualesquiera de las obligaciones asumidas en el presente pagaré notarial, La Deudora y el Fiador Solidario perderán el beneficio del término, haciéndose exigible la totalidad del DAR y en consecuencia, reconocen y autorizan que la primera copia del presente acto sea expedida en provecho de Avanzame, en calidad de título ejecutorio en su contra y en beneficio y provecho de Avanzame para la recuperación total del DAR, conforme a lo establecido por el Artículo545 del Código de Procedimiento Civil Dominicano; <b>SEPTIMO</b>: La Deudora y el Fiador Solidario renuncian al procedimiento de protesto (cláusula "sin protesto") y a la realización de toda diligencia, presentación al cobro, demanda, requerimiento de pago o notificación de no pago o falta de pago. Adicionalmente y en la medida en que ello sea permitido bajo la legislación aplicable, La Deudora y el Fiador Solidario renuncian a invocar la prescripción o caducidad como defensas a cualquier acción ejercida por Avanzame; <b>OCTAVO</b>: La falta de ejercicio de cualquiera de los derechos, acciones o recursos contenidos bajo el presente Pagaré por parte de Avanzame en cualquier momento y con respecto a cualquier situación particular no constituirá una renuncia de los mismos con respecto a ésa o a otras situaciones; <b>NOVENO</b>: La Deudora acepta y conviene que este Pagaré y los registros de contabilidad mantenidos por Avanzame en relación al saldo del DAR e intereses moratorios realizados por La Deudora, serán prueba suficiente y concluyente de los mismos salvo error manifiesto; <b>DECIMO</b>: El presente Pagaré se regirá por, e interpretará de acuerdo con, las leyes de la República Dominicana; <b>DÉCIMO PRIMERO</b>: La Deudora y el Fiador Solidario se comprometen a realizar el pago de cualquier tipo de impuestos que podrían generarse, inclusive los gastos de registro del presente pagaré; <b>DÉCIMO SEGUNDO</b>: La Deudora y el Fiador Solidario se obligan a pagar todos los costos de cobranza judicial o extrajudicial, incluyendo los honorarios de abogados, {00252846.DOC;1}
            incurridos por Avanzame en caso de incumplimiento por parte de La Deudora y/o del Fiador Solidario de sus obligaciones bajo el presente Pagaré. Para cualquier acción o procedimiento derivado o relativo al presente Pagaré, La Deudora y el Fiador Solidario se someten expresamente a la jurisdicción de los tribunales competentes de la República Dominicana, renunciando La Deudora y el Fiador Solidario a cualquier otra jurisdicción que pudiera corresponderle por razón de su domicilio presente o futuro, o por cualquier otra causa de cualquier naturaleza;; <b>DÉCIMO TERCERO</b>: El Fiador Solidario declara y reconoce que se constituye en Fiador Solidario e Indivisible de La Deudora para el cumplimiento de las obligaciones que preceden, comprometiéndose al pago solidario como si fueran La Deudora, renunciando al beneficio de excusión y división, al domicilio, al protesto, a la presentación del documento. HECHO Y FIRMADO el presente acto, el cual he leído en el lugar y fecha arriba indicados,
            acto que he leído íntegramente al compareciente,
            <xsl:value-of select="AuthorizedOwner/Name" />, en su ya indicada doble calidad de DEUDOR y FIADOR SOLIDARIO de <xsl:value-of select="LegalCompanyName" /> quien después de aprobarlo, lo ha firmado delante de mí y junto conmigo, escribiendo, de su puño y letra lo siguiente:
           <b>“BUENO Y VÁLIDO POR LA SUMA <xsl:value-of select="TotalOwnedAmountWords" /> (RD$ <xsl:value-of select="TotalOwnedAmount" />)</b> .
          </p>

          <p align="center">
            Espacio para escribir
          </p>
          <p align="center">
            ________________________________________________________
          </p>
          <p align="center">
            <xsl:value-of select="AuthorizedOwner/Name" />
          </p>
          <p align="center">
            A título personal en calidad de Fiador(a) Solidario(a) y representante de
          </p>
          <p align="center">
            <xsl:value-of select="LegalCompanyName" />
            <b>(La Deudora)</b>
          </p>
          <p align="center">
            ____________________________________
          </p>
          <p align="center">
            RODOLFO ANTONIO GUZMÁN GUZMÁN
            Notario Público
          </p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

