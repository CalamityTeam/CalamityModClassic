using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class GammaFusillade : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture = "CalamityModClassic1Point0/Items/Weapons/GammaFusillade");
		return true;
	}
	
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Gamma Fusillade");
        Item.damage = 35;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 3;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 3;
        Item.useAnimation = 3;
        Item.useStyle = 5;
        //Tooltip.SetDefault("Unleashes a concentrated beam of gamma radiation");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3;
        Item.value = 1250000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item33;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("GammaLaser").Type;
        Item.shootSpeed = 20f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 8);
        recipe.AddIngredient(null, "BarofLife", 3);
        recipe.AddIngredient(null, "CoreofCalamity");
        recipe.AddIngredient(ItemID.SpellTome, 1);
        recipe.AddIngredient(ItemID.SoulofMight, 50);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}