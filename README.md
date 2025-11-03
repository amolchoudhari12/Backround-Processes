# Vilani Language Translation Background Service

This project implements a **Windows Background Service** designed for the **real estate domain** to automatically detect and fix **missing translated text** across multilingual database tables.  
It proactively runs translation jobs at scheduled intervals, ensuring that multilingual data (English, Marathi, Hindi, etc.) remains synchronized and complete — without manual intervention from users.

---

## 🧠 Overview

The service is part of the **Vilani Platform**, which powers real estate listings and property management portals.  
It integrates with the **Vilani Language Translation Layer**, continuously monitoring domain tables for untranslated or missing localized content, and automatically updating them via the internal **Vilani Translator Services API**.

This automation removes the need for manual translation maintenance, reducing operational overhead and ensuring accurate multilingual data presentation across all builder and project websites.

---

## 🧩 Core Components

- **VilaniLanguageTranslatorFasade** – Central façade responsible for orchestrating translation across multiple domain entities.  
- **PlottingSchemeTranslator, PropertyTranslator, DeveloperTranslator**, etc. – Specialized translators handling individual domain tables (e.g., properties, developers, locations).  
- **VilaniTranslatorServicesClient** – Service reference that communicates with the translation web service (`ConvertToMarathi`, `ConvertToEnglishArray`, etc.).  
- **Background Scheduler** – Invokes translation routines at regular intervals to keep database translations up to date.

---

## ⚙️ How It Works

1. The service scans domain tables (projects, developers, properties, locations, etc.) for missing or outdated translations.  
2. It uses the **Vilani Translator Service** to translate missing content automatically.  
3. Each translator (e.g., `ProjectTranslator`, `PropertyTranslator`) processes its specific entity set.  
4. Logs or console outputs confirm successful updates and performance timings.  

Sample usage:
```csharp
VilaniLanguageTranslatorFasade translator = new VilaniLanguageTranslatorFasade();
translator.TranslatePlottingDomainTables();
```

---

## 🎯 Benefits

- Eliminates manual translation efforts.  
- Keeps multilingual content current across all builder websites.  
- Scalable for additional real estate domain entities.  
- Integrates seamlessly with Vilani’s dynamic multi-language platform.

---

## 👨‍💻 Author

Developed by **Amol**  
Part of the **Vilani Real Estate Automation Suite**, enabling proactive, self-healing multilingual database synchronization.
