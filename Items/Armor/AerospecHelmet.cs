using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class AerospecHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Aerospec Helmet");
        //Tooltip.SetDefault("8% increased summon damage");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 50000;
        Item.rare = ItemRarityID.Orange;
        Item.defense = 2; //13
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("AerospecBreastplate").Type && legs.type == Mod.Find<ModItem>("AerospecLeggings").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Increased movement speed as health decreases\n" +
        	"Summons a valkyrie to protect you";
        CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.valkyrie = true;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(Mod.Find<ModBuff>("Valkyrie").Type) == -1)
			{
				player.AddBuff(Mod.Find<ModBuff>("Valkyrie").Type, 3600, true);
			}
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Valkyrie").Type] < 1)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("Valkyrie").Type, 25, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.moveSpeed += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.moveSpeed += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.moveSpeed += 0.15f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.moveSpeed += 0.2f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += 0.08f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 5);
        recipe.AddIngredient(ItemID.Cloud, 3);
        recipe.AddIngredient(ItemID.RainCloud);
        recipe.AddIngredient(ItemID.Feather);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}}