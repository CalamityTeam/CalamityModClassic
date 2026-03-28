using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons.AbyssWeapons
{
	public class EidolicWail : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eidolic Wail");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 210;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 60;
	        Item.height = 60;
            Item.useTime = 12;
            Item.reuseDelay = 30;
            Item.useAnimation = 36;
            Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 1f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/WyrmScream");
	        Item.autoReuse = true;
	        Item.shootSpeed = 5f;
	        Item.shoot = Mod.Find<ModProjectile>("EidolicWail").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
	}
}