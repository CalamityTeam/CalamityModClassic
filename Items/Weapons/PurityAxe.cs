using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PurityAxe : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/PurityAxe");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Axe of Purity");
        Item.damage = 43;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 46;
        Item.height = 44;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.useTurn = true;
        Item.axe = 25;
        Item.useStyle = 1;
        Item.knockBack = 7.5f;
        Item.value = 300000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "FellerofEvergreens");
        recipe.AddIngredient(ItemID.PixieDust, 10);
        recipe.AddIngredient(ItemID.CrystalShard, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 58);
        }
    }
}}