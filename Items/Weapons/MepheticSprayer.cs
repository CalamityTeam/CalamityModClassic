using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class MepheticSprayer : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/8.Plaguebringer/MepheticSprayer");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Blight Spewer");
			Item.damage = 99;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 76;
			Item.height = 36;
			////Tooltip.SetDefault("Consumes gel\nHighly effective against almost everything");
			Item.useTime = 10;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item34;
			Item.value = 1200000;
			Item.rare = 8;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("CorossiveFlames").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 7.5f;
			Item.useAmmo = 23;
		}
	}
}