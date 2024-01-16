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
	public class ElementalBlaster : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/ElementalBlaster");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Elemental Blaster");
			Item.damage = 98;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 92;
			Item.height = 28;
			Item.useTime = 2;
			////Tooltip.SetDefault("Does not consume ammo\nFires a storm of rainbow blasts");
			Item.useAnimation = 6;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 1.75f;
			Item.value = 1000000;
			Item.rare = 10;
			Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point1/Sounds/Item/PlasmaBolt");
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("RainbowBlast").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 24f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SpectralstormCannon");
            recipe.AddIngredient(null, "ClockGatlignum");
            recipe.AddIngredient(null, "PaintballBlaster");
            recipe.AddIngredient(null, "GalacticaSingularity", 5);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}